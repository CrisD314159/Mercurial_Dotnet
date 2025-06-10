using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IUserService
{
  Task CreateUser(CreateUserDTO createUserDTO);

  Task UpdateUser(string Id, UpdateUserDTO updateUserDTO);

  Task<GetUserDTO> GetUserOverview(string userId);

  Task DeleteUser(string userId);

  Task VerifyUser(VerifyuserDTO verifyuserDTO);

  Task RecoverAccount(RecoverAccountDTO recoverAccountDTO);

  Task ChangePassword(ChangePasswordDTO changePasswordDTO);
}