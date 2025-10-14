
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface IGetUserOverviewUseCase
{
  Task<GetUserDTO> Execute(string userId);
}