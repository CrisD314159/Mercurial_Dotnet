using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.Account.UseCasesInterfaces;

public interface IRefreshTokenUseCase
{
  Task<RefreshTokenResponseDTO> Execute(string refreshToken);
}
