using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class UserService(MercurialDBContext dBContext) : IUserService
{
  private readonly MercurialDBContext _dbContext = dBContext;
  public void CreateUser(CreateUserDTO createUserDTO)
  {
    throw new NotImplementedException();
  }

  public void DeleteUser(string userId)
  {
    throw new NotImplementedException();
  }

  public GetUserDTO GetUserOverview(string userId)
  {
    throw new NotImplementedException();
  }

  public void UpdateUser(UpdateUserDTO updateUserDTO)
  {
    throw new NotImplementedException();
  }

  public void VerifyUser(VerifyuserDTO verifyuserDTO)
  {
    throw new NotImplementedException();
  }
}