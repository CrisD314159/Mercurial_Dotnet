using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;


public class VerifyValidAssignmentService(IAssignmentRepository assignmentRepository): IVerifyValidAssignmentService
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
  public async Task<bool> Execute(string userId, string title)
  {
    if (await _assignmentRepository.UserHasExeededMaximumAssignments(userId))
      throw new ExceededLimitException("You've reached your maximum ammount of assignments");

     var assignment = await _assignmentRepository.GetAssignmentByAssignmentNameUserIdAndState(title, userId, Domain.Enums.AssignmentState.TODO);
    if (assignment != null)
      throw new VerificationException($"There's already an assignment with name {title}");

    return true;
  }
}