using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IUserService
{
  Task CreateUser(CreateUserDTO createUserDTO);

  Task UpdateUser(UpdateUserDTO updateUserDTO);

  Task<GetUserDTO> GetUserOverview(string userId);

  Task DeleteUser(string userId);

  Task VerifyUser(VerifyuserDTO verifyuserDTO);
}