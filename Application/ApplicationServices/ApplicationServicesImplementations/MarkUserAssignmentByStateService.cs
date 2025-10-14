using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;

public class MarkUserAssignmentByStateService(
IAssignmentRepository assignmentRepository
): IMarkUserAssignmentByStateService
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;

  public async Task Execute(string userId, Guid assignmentId, AssignmentState state)
  {
    var assignment = await _assignmentRepository.GetAssignmentByAssignmentIdAndUserId(assignmentId, userId);
    if (assignment.TaskState == state) throw new EntityValidationException($"Assignment is already marked as {state.ToString().ToLower()}");
    assignment.TaskState = state;

    await _assignmentRepository.UpdateAssingmentAsync(assignment);
  }
}