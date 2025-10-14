using MercurialBackendDotnet.Domain.Enums;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IMarkUserAssignmentByStateService
{
  Task Execute(string userId, Guid assignmentId, AssignmentState state);
}