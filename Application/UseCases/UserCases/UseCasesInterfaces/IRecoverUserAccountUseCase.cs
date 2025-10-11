
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface IRecoverUserAccountUseCase
{
  Task Execute(RecoverAccountDTO recoverAccountDTO);
}