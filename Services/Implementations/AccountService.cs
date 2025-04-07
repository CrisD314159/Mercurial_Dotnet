using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Utils;
using Microsoft.EntityFrameworkCore;

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

  public async Task SendAccountCreatedVerificationCode(string name, string email, int code)
  {
    await EmailUtil.ReadFileToSendEmail(name, "Verify your email", email, "Thanks for sign up to Mercurial, use this code to verify your account", 
    code);
  }

  public async Task SendRecoverAccountVerificationCode(string name, string email, int code)
  {
    await EmailUtil.ReadFileToSendEmail(name, "Recover your account", email, "Use this code to recover your account :)", code);
  }

  public Task UpdateAccount(UpdateAccountDTO updateAccountDTO)
  {
    throw new NotImplementedException();
  }

  public async Task<bool> VerifyCode(string email, int code)
  {
    var user = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email && a.VerificationCode == code)
    ?? throw new EntityNotFoundException("Account not found");
    return true;
  }
}