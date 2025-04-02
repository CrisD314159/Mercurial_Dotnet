using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Model;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAccountService
{
  Task<Account>  CreateAccount(User user, string email, string password);

  Task UpdateAccount(UpdateAccountDTO updateAccountDTO);

  Task<LoginResponseDTO> Login(LoginDTO loginDTO);

  Task Logout(string refreshToken);

  Task SendAccountCreatedVerificationCode(string email);

  Task SendRecoverAccountVerificationCode(string email);

  Task<bool> VerifyCode(string email, int code);

  
}