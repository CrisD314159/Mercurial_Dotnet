
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface IVerifyUserUseCase
{
  Task Execute(VerifyuserDTO verifyuserDTO);
}