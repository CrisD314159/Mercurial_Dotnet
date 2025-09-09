using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases;

public class CreateThirdPartyUser(UserManager<User> userManager, ValidateAndGenerateValidUsernameService validateAndGenerateValidUsernameService)
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly ValidateAndGenerateValidUsernameService _validateAndGenerateValidUsernameService = validateAndGenerateValidUsernameService;

  public async Task<User> Execute(string email, string name)
  {
    if (await _userManager.FindByEmailAsync(email) != null)
      throw new EntityAlreadyExistsException("User already exists");

    var validAndUniqueUsername = await _validateAndGenerateValidUsernameService.Execute(name);
    User user = new()
    {
      Name = name,
      State = UserState.ACTIVE,
      ProfilePicture = $"https://api.dicebear.com/9.x/thumbs/svg?seed={validAndUniqueUsername}",
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
      VerificationCode = "",
      UserName = validAndUniqueUsername,
      Email = email,
      EmailConfirmed = true,
      IsThirdPartyUser = true
    };

    var result = await _userManager.CreateAsync(user, "Dummy-User1");
    if (!result.Succeeded)
    {
      throw new InternalServerException(string.Join(";", result.Errors.Select(e => e.Description)));
    }

    return user;
  }

  
}