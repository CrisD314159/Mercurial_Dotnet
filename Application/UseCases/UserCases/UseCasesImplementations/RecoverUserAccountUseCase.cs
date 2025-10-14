using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesImplementations;

public class RecoverUserAccountUseCase(UserManager<User> userManager, IEmailService emailService): IRecoverUserAccountUseCase
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly IEmailService _emailService = emailService;
  
  public async Task Execute(RecoverAccountDTO recoverAccountDTO)
  {
    var user = await _userManager.FindByEmailAsync(recoverAccountDTO.Email)
      ?? throw new EntityNotFoundException("User does not exists");

    if (user.IsThirdPartyUser) throw new EntityValidationException("Use your Google account to log in");

    var recoveryCode = await _userManager.GeneratePasswordResetTokenAsync(user);
    var encodedCode = Uri.EscapeDataString(recoveryCode);
    var link = $"https://mercurial-app.vercel.app/changePassword?changeToken={encodedCode}";
    await _emailService.SendRecoverAccountVerificationCode(user.Name, recoverAccountDTO.Email, link);

  }
}