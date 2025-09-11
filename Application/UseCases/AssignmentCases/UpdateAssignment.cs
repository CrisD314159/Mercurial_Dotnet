using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases;

public class UpdateAssignment(
IAssignmentRepository assignmentRepository,
ISubjectRepository subjectRepository,
ITopicRepository topicRepository
)
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  private readonly ITopicRepository _topicRepository = topicRepository;
public async Task Execute(string userId, UpdateAssignmentDTO updateAssignmentDTO)
  {
    var assignment = await _assignmentRepository.GetAssignmentByAssignmentIdAndUserId(updateAssignmentDTO.AssignmentId, userId);

    if (assignment.SubjectId != updateAssignmentDTO.SubjectId)
    {
      var subject = await _subjectRepository.GetSubjectBySubjectIdAndUserId(updateAssignmentDTO.SubjectId, userId);
      assignment.Subject = subject;
    }
    if(assignment.TopicId != updateAssignmentDTO.TopicId)
    {
      var topic = await _topicRepository.GetTopicByIdAndUserId(updateAssignmentDTO.TopicId, userId);
      assignment.Topic = topic;
    }
    if (assignment.Note != null)
    {
        assignment.Note.Content = updateAssignmentDTO.NoteContent;
    }
    assignment.DueDate = updateAssignmentDTO.DueDate;
    assignment.Title = updateAssignmentDTO.Title;
    assignment.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _assignmentRepository.UpdateAssingmentAsync(assignment);
  }
}