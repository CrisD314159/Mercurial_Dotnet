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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MercurialBackendDotnet.Services.Implementations;

public class AssignmentService (MercurialDBContext dBContext, IValidator<CreateAssignmentDTO> validatorCreate,
IValidator<UpdateAssignmentDTO> validatorUpdate, UserManager<User> userManager) : IAssignmentService
{
  private readonly MercurialDBContext _dbContext = dBContext;
  private readonly IValidator<CreateAssignmentDTO> _validatorCreate = validatorCreate;
  private readonly IValidator<UpdateAssignmentDTO> _validatorUpdate = validatorUpdate;
  private readonly UserManager<User> _userManager = userManager;

  /// <summary>
  /// Creates a Assignment
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="createAssignmentDTO"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task CreateAssignment(string userId, CreateAssignmentDTO createAssignmentDTO)
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
          User = await _userManager.FindByIdAsync(userId) 
          ?? throw new EntityNotFoundException("User not found"),
          DueDate = createAssignmentDTO.DueDate,
          TaskState = AssignmentState.TODO,
          LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
          HasChecklist = false
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

  /// <summary>
  /// Verfies if an assignment is able to create 
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="title"></param>
  /// <returns></returns>
  /// <exception cref="ExceededLimitException"></exception>
  /// <exception cref="VerificationException"></exception>
  private async Task<bool> VerifyValidAssignment(string userId, string title)
  {
    if(_dbContext.Assignments.Where(a => a.UserId == userId).Count() >= 700) 
    throw new ExceededLimitException("You've reached your maximum ammount of assignments");

    if(await _dbContext.Assignments.AnyAsync(a => a.UserId == userId && a.Title == title))
    throw new VerificationException($"There's already an assignment with name {title}");

    return true;
  }

  public async Task DeleteAssignment(string userId, Guid assignmentId)
  {
    var assignment = await _dbContext.Assignments.Include(a => a.Note)
    .FirstOrDefaultAsync(a => a.Id == assignmentId && a.UserId == userId)
    ?? throw new EntityNotFoundException("Assignment not found");

    _dbContext.Assignments.Remove(assignment);
    await _dbContext.SaveChangesAsync();

  }

  /// <summary>
  /// Gets the user assignments with the Done tasks
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="offset"></param>
  /// <param name="limit"></param>
  /// <returns></returns>
  public async Task<GetUserAssignmentsDTO> GetUserDoneTasks(string userId, int offset, int limit)
  { 
    return await GetUserTasks(userId, offset, limit, AssignmentState.DONE);
  }

  /// <summary>
  /// Gets the user assignment with the Todo flag
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="offset"></param>
  /// <param name="limit"></param>
  /// <returns></returns>
  public async Task<GetUserAssignmentsDTO> GetUserTodoTasks(string userId, int offset, int limit)
  {
   return await GetUserTasks(userId, offset, limit, AssignmentState.TODO);
  }

  /// <summary>
  /// Gets the user tasks according to the param flag 
  /// This method unifies both get done and todo assignments
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="offset"></param>
  /// <param name="limit"></param>
  /// <param name="state"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  /// <exception cref="UnauthorizedException"></exception>
  private async Task<GetUserAssignmentsDTO> GetUserTasks(string userId, int offset, int limit, AssignmentState state)
  {
   var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");

    if(!await _userManager.IsEmailConfirmedAsync(user)) throw new UnauthorizedException("You're not verified");
    var assignments = await _dbContext.Assignments.Include(a=> a.Note).Where(a => a.UserId == userId && a.TaskState == state)
    .OrderByDescending(a => a.CreatedAt)
    .Select(a => new AssignmentDTO(
      a.Id, a.Title, a.LastUpdatedAt, a.DueDate, a.TaskState,
      a.SubjectId, a.TopicId, a.Note.Id, a.Note.Content ?? ""
    )).Skip(offset).Take(limit).ToListAsync();

    return new GetUserAssignmentsDTO(assignments);
  }


  /// <summary>
  /// Updates an assignment
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="updateTaskDTO"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task UpdateAssignment(string userId, UpdateAssignmentDTO updateTaskDTO)
  {
    var assignment = await _dbContext.Assignments.Include(a => a.Note)
    .FirstOrDefaultAsync(a => a.Id == updateTaskDTO.AssignmentId && a.UserId == userId)
     ?? throw new EntityNotFoundException("Assignment not found");

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
    assignment.Title = updateTaskDTO.Title;
    assignment.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Marks an assignment with the flag Done
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="assignmentId"></param>
  /// <returns></returns>
  public async Task MarkAssignmentAsDone(string userId, Guid assignmentId)
  {
    await SetAssignmentState(userId, assignmentId, AssignmentState.DONE);
  }

  /// <summary>
  /// Marks an assignment with in progress flag
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="assignmentId"></param>
  /// <returns></returns>
  public async Task MarkAssigmentInprogress(string userId, Guid assignmentId)
  {
    await SetAssignmentState(userId, assignmentId, AssignmentState.IN_PROGRESS);
  }

  /// <summary>
  /// Marks an assignment with todo flag
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="assignmentId"></param>
  /// <returns></returns>
  public async Task MarkAssigmentTodo(string userId, Guid assignmentId)
  {
    await SetAssignmentState(userId, assignmentId, AssignmentState.TODO);
  }

  /// <summary>
  /// Modifies the state assignment value with the state param specified
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="assignmentId"></param>
  /// <param name="state"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  private async Task SetAssignmentState(string userId, Guid assignmentId, AssignmentState state)
  {
    var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.Id == assignmentId && a.UserId ==userId)
    ?? throw new EntityNotFoundException("Assignment not found");
    assignment.TaskState = state;

    await _dbContext.SaveChangesAsync();
  }

}