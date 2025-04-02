using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IUserService
{
  Task CreateUser(CreateUserDTO createUserDTO);

  Task UpdateUser(Guid Id, UpdateUserDTO updateUserDTO);

  Task<GetUserDTO> GetUserOverview(Guid userId);

  Task DeleteUser(Guid userId);

  Task VerifyUser(VerifyuserDTO verifyuserDTO);
}