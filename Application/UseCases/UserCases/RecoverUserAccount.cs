using System.Security;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Application.UseCases.UserCases;

public class RecoverUserAccount(UserManager<User> userManager, IEmailService emailService)
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly IEmailService _emailService = emailService;
  
  public async Task Execute(RecoverAccountDTO recoverAccountDTO)
  {
    var user = await _userManager.FindByEmailAsync(recoverAccountDTO.Email)
      ?? throw new EntityNotFoundException("User does not exists");

    if (user.IsThirdPartyUser) throw new VerificationException("Use your Google account to log in");

    var recoveryCode = await _userManager.GeneratePasswordResetTokenAsync(user);

    await _emailService.SendRecoverAccountVerificationCode(user.Name, recoverAccountDTO.Email, recoveryCode);

  }
}