using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Model.Enums;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Services.Implementations;

public class ThirdPartyAccountService(UserManager<User> userManager) : IThirdPartyAccountService
{

  private readonly UserManager<User> _userManager = userManager;


  /// <summary>
  /// Creates a new user using a third party authenticator like Google or facebook
  /// </summary>
  /// <param name="email"></param>
  /// <param name="name"></param>
  /// <param name="profilePicture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public async Task<User> CreateThirdPartyUser(string email, string name)
  {
    if (await _userManager.FindByEmailAsync(email) != null)
      throw new EntityAlreadyExistsException("User already exists");

    var cleanName = name.Trim().Replace(" ", "");
    User user = new()
    {
      Id = Guid.NewGuid().ToString(),
      Name = name,
      State = UserState.ACTIVE,
      ProfilePicture = $"https://api.dicebear.com/9.x/thumbs/svg?seed={name}",
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
      VerificationCode = new Random().Next(1000, 9999).ToString(),
      UserName = cleanName,
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