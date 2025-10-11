using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.Account.UseCasesInterfaces;

public interface ILogoutUseCase
{
  Task Execute(string refreshToken);
}