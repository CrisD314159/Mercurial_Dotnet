using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class AccountService(MercurialDBContext dbContext) : IAccountService
{

  private readonly MercurialDBContext _dbContext = dbContext;

  public Task<Account> CreateAccount(User user, string email, string password)
  {
    throw new NotImplementedException();
  }

  public Task<LoginResponseDTO> Login(LoginDTO loginDTO)
  {
    throw new NotImplementedException();
  }

  public Task Logout(string refreshToken)
  {
    throw new NotImplementedException();
  }

  public Task SendAccountCreatedVerificationCode(string email)
  {
    throw new NotImplementedException();
  }

  public Task SendRecoverAccountVerificationCode(string email)
  {
    throw new NotImplementedException();
  }

  public Task UpdateAccount(UpdateAccountDTO updateAccountDTO)
  {
    throw new NotImplementedException();
  }

  public Task<bool> VerifyCode(string email, int code)
  {
    throw new NotImplementedException();
  }
}