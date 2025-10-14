using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface ICreateRegularUserUseCase
{
  Task Execute(CreateUserDTO createUserDTO);
}