using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IGetUserAssignmentsByStateService
{
  Task<GetUserAssignmentsDTO> Execute(string userId, int offset, int limit, AssignmentState state);
}