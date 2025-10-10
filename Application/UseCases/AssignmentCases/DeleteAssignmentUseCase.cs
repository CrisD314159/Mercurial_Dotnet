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

public class DeleteAssignmentUseCase(
IAssignmentRepository assignmentRepository
)
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
  public async Task Execute(string userId, Guid assignmentId)
  {
    var assignment = await _assignmentRepository.GetAssignmentByAssignmentIdAndUserId(assignmentId, userId)
      ?? throw new EntityNotFoundException("Assignment not found");

    await _assignmentRepository.DeleteAssignmentAsync(assignment);

  }
}