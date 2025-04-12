using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Model;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAccountService
{
  Task RefreshToken();

  Task UpdateAccount(UpdateAccountDTO updateAccountDTO);

  Task<LoginResponseDTO> Login(LoginDTO loginDTO);

  Task Logout(string refreshToken);

  Task SendAccountCreatedVerificationCode(string name, string email, string code);

  Task SendRecoverAccountVerificationCode(string name, string email, string code);

  Task<bool> VerifyCode(string email, int code);

  
}