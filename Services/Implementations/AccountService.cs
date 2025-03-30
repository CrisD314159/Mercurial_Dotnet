using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class AccountService(MercurialDBContext dbContext) : IAccountService
{

  private readonly MercurialDBContext _dbContext = dbContext;

  public Account CreateAccount(User user, string email, string password)
  {
    throw new NotImplementedException();
  }

  public LoginResponseDTO Login(LoginDTO loginDTO)
  {
    throw new NotImplementedException();
  }

  public void Logout(string refreshToken)
  {
    throw new NotImplementedException();
  }

  public void SendAccountCreatedVerificationCode(string email)
  {
    throw new NotImplementedException();
  }

  public void SendRecoverAccountVerificationCode(string email)
  {
    throw new NotImplementedException();
  }

  public void UpdateAccount(UpdateAccountDTO updateAccountDTO)
  {
    throw new NotImplementedException();
  }

  public bool VerifyCode(string email, int code)
  {
    throw new NotImplementedException();
  }
}