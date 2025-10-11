using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface IChangeUserPasswordUseCase
{
  Task Execute(ChangePasswordDTO changePasswordDTO);
}