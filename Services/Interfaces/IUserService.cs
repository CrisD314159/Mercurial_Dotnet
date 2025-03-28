using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IUserService
{
  void CreateUser(CreateUserDTO createUserDTO);

  void UpdateUser(UpdateUserDTO updateUserDTO);

  GetUserDTO GetUserOverview(string userId);

  void DeleteUser(string userId);

  void VerifyUser(VerifyuserDTO verifyuserDTO);
}