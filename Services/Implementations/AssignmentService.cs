using FluentValidation;
using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Model.Enums;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MercurialBackendDotnet.Services.Implementations;

public class AssignmentService (MercurialDBContext dBContext, IValidator<CreateAssignmentDTO> validatorCreate,
IValidator<UpdateAssignmentDTO> validatorUpdate) : IAssignmentService
{
  private readonly MercurialDBContext _dbContext = dBContext;
  private readonly IValidator<CreateAssignmentDTO> _validatorCreate = validatorCreate;
  private readonly IValidator<UpdateAssignmentDTO> _validatorUpdate = validatorUpdate;

  public async Task CreateAssignment(Guid userId, CreateAssignmentDTO createAssignmentDTO)
  {
   _validatorCreate.ValidateAndThrow(createAssignmentDTO);

    if(await VerifyValidAssignment(userId, createAssignmentDTO.Title))
    {
      Assignment assignment = new(){
          Title = createAssignmentDTO.Title,
          Subject = await _dbContext.Subjects.FindAsync(createAssignmentDTO.SubjectId) 
          ?? throw new EntityNotFoundException("Subject not found"),
          Topic = await _dbContext.Topics.FindAsync(createAssignmentDTO.TopicId)
          ?? throw new EntityNotFoundException("Topic not found"),
          User = await _dbContext.Users.FindAsync(userId) 
          ?? throw new EntityNotFoundException("User not found"),
          DueDate = createAssignmentDTO.DueDate,
          TaskState = AssignmentState.TODO,
          LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
      };
      Note note = new(){
        Content =  createAssignmentDTO.NoteContent,
        Assignment = assignment
      };
      assignment.Note = note;
      await _dbContext.Notes.AddAsync(note);
      await _dbContext.Assignments.AddAsync(assignment);
      await _dbContext.SaveChangesAsync();
    }
  }

  private async Task<bool> VerifyValidAssignment(Guid userId, string title)
  {
    if(_dbContext.Assignments.Where(a => a.UserId == userId).Count() > 700) 
    throw new ExceededLimitException("You've reached your maximum ammount of assignments");

    if(await _dbContext.Assignments.AnyAsync(a => a.UserId == userId && a.Title == title))
    throw new VerificationException($"There's already an assignment with name {title}");

    return true;
  }

  public async Task DeleteAssignment(Guid taskId)
  {
    var assignment = await _dbContext.Assignments.Include(a => a.Note).Where(a => a.Id == taskId).FirstOrDefaultAsync()
    ?? throw new EntityNotFoundException("Assignment not found");

    _dbContext.Assignments.Remove(assignment);
    await _dbContext.SaveChangesAsync();

  }

  public async Task<GetUserAssignmentsDTO> GetUserDoneTasks(Guid userId, int offset, int limit)
  { 
    return await GetUserTasks(userId, offset, limit, AssignmentState.DONE);
  }

  public async Task<GetUserAssignmentsDTO> GetUserTodoTasks(Guid userId, int offset, int limit)
  {
   return await GetUserTasks(userId, offset, limit, AssignmentState.TODO);
  }

  private async Task<GetUserAssignmentsDTO> GetUserTasks(Guid userId, int offset, int limit, AssignmentState state)
  {
    if (!await _dbContext.Users.AnyAsync(u => u.Id == userId )) throw new EntityNotFoundException("User not found");
    var assignments = await _dbContext.Assignments.Include(a=> a.Note).Where(a => a.UserId == userId && a.TaskState == state)
    .Select(a => new AssignmentDTO(
      a.Id, a.Title, a.LastUpdatedAt, a.DueDate, 
      a.SubjectId, a.TopicId, a.Note.Id, a.Note.Content ?? ""
    )).Skip(offset).Take(limit).ToListAsync();

    return new GetUserAssignmentsDTO(assignments);
  }

  public async Task UpdateAssignment(UpdateAssignmentDTO updateTaskDTO)
  {
    var assignment = await _dbContext.Assignments.Include(a => a.Note).Where(a => a.Id == updateTaskDTO.AssignmentId)
    .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Assignment not found");

    if(assignment.SubjectId != updateTaskDTO.SubjectId)
    {
      var subject = await _dbContext.Subjects.FindAsync(updateTaskDTO.SubjectId) 
      ?? throw new EntityNotFoundException("Subject not found");
      assignment.Subject = subject;
    }
    if(assignment.TopicId != updateTaskDTO.TopicId)
    {
      var topic = await _dbContext.Topics.FindAsync(updateTaskDTO.TopicId) 
      ?? throw new EntityNotFoundException("Topic not found");
      assignment.Topic = topic;
    }
    if (assignment.Note != null)
    {
        assignment.Note.Content = updateTaskDTO.NoteContent;
    }
    assignment.DueDate = updateTaskDTO.DueDate;
    assignment.Title = assignment.Title;
    assignment.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _dbContext.SaveChangesAsync();
  }

  public async Task MarkAssignmentAsDone(Guid assignmentId)
  {
    await SetAssignmentState(assignmentId, AssignmentState.DONE);
  }

  public async Task MarkAssigmentInprogress(Guid assignmentId)
  {
    await SetAssignmentState(assignmentId, AssignmentState.IN_PROGRESS);
  }

  public async Task MarkAssigmentTodo(Guid assignmentId)
  {
    await SetAssignmentState(assignmentId, AssignmentState.TODO);
  }

  private async Task SetAssignmentState(Guid assignmentId, AssignmentState state)
  {
    var assignment = await _dbContext.Assignments.FindAsync(assignmentId)
    ?? throw new EntityNotFoundException("Assignment not found");
    assignment.TaskState = state;

    await _dbContext.SaveChangesAsync();
  }

}