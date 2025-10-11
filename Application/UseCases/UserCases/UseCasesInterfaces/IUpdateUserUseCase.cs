
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface IUpdateUserUseCase
{
  Task Execute(string id, UpdateUserDTO updateUserDTO);
}