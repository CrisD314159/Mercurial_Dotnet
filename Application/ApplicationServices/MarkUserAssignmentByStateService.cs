using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.ApplicationServices;

public class MarkUserAssignmentByStateService(
IAssignmentRepository assignmentRepository
)
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;

  public async Task Execute(string userId, Guid assignmentId, AssignmentState state)
  {
    var assignment = await _assignmentRepository.GetAssignmentByAssignmentIdAndUserId(assignmentId, userId);
    if (assignment.AssignmentState == state) throw new EntityValidationException($"Assignment is already marked as {state.ToString().ToLower()}");
    assignment.AssignmentState = state;

    await _assignmentRepository.UpdateAssingmentAsync(assignment);
  }
}