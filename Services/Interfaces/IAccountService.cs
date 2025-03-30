using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Model;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAccountService
{
  Account  CreateAccount(User user, string email, string password);

  void UpdateAccount(UpdateAccountDTO updateAccountDTO);

  LoginResponseDTO Login(LoginDTO loginDTO);

  void Logout(string refreshToken);

  void SendAccountCreatedVerificationCode(string email);

  void SendRecoverAccountVerificationCode(string email);

  bool VerifyCode(string email, int code);

  
}