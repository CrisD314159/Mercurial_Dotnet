using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesImplementations;


public class VerifyUserUseCase(UserManager<User> userManager): IVerifyUserUseCase
{
  private readonly UserManager<User> _userManager = userManager;
  public async Task Execute(VerifyuserDTO verifyuserDTO)
  {
    var user = await _userManager.FindByEmailAsync(verifyuserDTO.Email)
      ?? throw new EntityNotFoundException("User does not exists");

    if (await _userManager.IsEmailConfirmedAsync(user)) throw new VerificationException("User is already verified");

    if (user.VerificationCode != verifyuserDTO.Code)
      throw new VerificationException("Invalid code or email");

    user.EmailConfirmed = true;
    user.State = UserState.ACTIVE;
    user.VerificationCode = "0";
    user.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      throw new InternalServerException("An error occurred while trying to verify your account");
    }
  }
}