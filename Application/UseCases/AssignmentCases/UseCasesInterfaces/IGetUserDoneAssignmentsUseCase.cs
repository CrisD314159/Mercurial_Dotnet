using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;

public interface IGetUserDoneAssignmentsUseCase
{
  Task<GetUserAssignmentsDTO> Execute(string userId, int offset, int limit);
}