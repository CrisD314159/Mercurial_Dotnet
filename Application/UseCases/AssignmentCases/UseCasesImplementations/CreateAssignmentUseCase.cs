using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;


namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesImplementations;

public class CreateAssignmentUseCase(IValidator<CreateAssignmentDTO> validator,
IVerifyValidAssignmentService verifyValidAssignmentService,
IAssignmentRepository assignmentRepository,
ISubjectRepository subjectRepository,
ITopicRepository topicRepository,
UserManager<User> userManager
): ICreateAssignmentUseCase
{
  private readonly IValidator<CreateAssignmentDTO> _validator = validator;
  private readonly IVerifyValidAssignmentService _verifyValidAssignment= verifyValidAssignmentService;
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  private readonly ITopicRepository _topicRepository = topicRepository;
  private readonly UserManager<User> _userManager = userManager ;
  public async Task Execute(string userId, CreateAssignmentDTO createAssignmentDTO)
  {
    _validator.ValidateAndThrow(createAssignmentDTO);

    if (await _verifyValidAssignment.Execute(userId, createAssignmentDTO.Title))
    {
      var user = await _userManager.FindByIdAsync(userId)
        ?? throw new EntityNotFoundException("User not found");

      if (!user.EmailConfirmed) throw new EntityValidationException("You have not verified your account");

      var subject = await _subjectRepository.GetSubjectBySubjectIdAndUserId(createAssignmentDTO.SubjectId, userId)
        ?? throw new EntityNotFoundException("Subject not found");
      var topic = await _topicRepository.GetTopicByIdAndUserId(createAssignmentDTO.TopicId, userId)
        ?? throw new EntityNotFoundException("Topic not found");
      Assignment assignment = new()
      {
        Title = createAssignmentDTO.Title,
        Subject = subject,
        Topic = topic,
        User = user,
        DueDate = createAssignmentDTO.DueDate,
        TaskState = AssignmentState.TODO,
        LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
        HasChecklist = false
      };
      Note note = new()
      {
        Content = createAssignmentDTO.NoteContent,
        Assignment = assignment
      };
      assignment.Note = note;
      await _assignmentRepository.CreateAssignmentAsync(assignment, note);
    }
  }
}