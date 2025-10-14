using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.Account.UseCasesInterfaces;

public interface ILoginUsingGoogleUseCase
{
  Task<LoginResponseDTO> Execute(string email, string name);
}