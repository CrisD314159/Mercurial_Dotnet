
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;


public class VerifyValidAssignmentService(IAssignmentRepository assignmentRepository): IVerifyValidAssignmentService
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
  public async Task<bool> Execute(string userId, string title, long subjectId)
  {
    Assignment assignment;

    if (await _assignmentRepository.UserHasExeededMaximumAssignments(userId))
      throw new ExceededLimitException("You've reached your maximum ammount of assignments");
    try
    {
      assignment = await _assignmentRepository
        .GetAssignmentByAssignmentNameUserIdAndState(title, userId, Domain.Enums.AssignmentState.TODO, subjectId);
    }
    catch (EntityNotFoundException)
    {
      return true;
    }
    if (assignment != null)
      throw new EntityValidationException($"There's already an assignment with name {title}");
    
    return true;
  }
}