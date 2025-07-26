using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Model.Enums;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Services.Implementations;

public partial class ThirdPartyAccountService(UserManager<User> userManager) : IThirdPartyAccountService
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

    var cleanName = CleanString(name);

    var uniqueUsername = await GenerateUniqueUsername(cleanName);
    User user = new()
    {
      Name = name,
      State = UserState.ACTIVE,
      ProfilePicture = $"https://api.dicebear.com/9.x/thumbs/svg?seed={uniqueUsername}",
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
      VerificationCode = "",
      UserName = uniqueUsername,
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

    public async Task<string> GenerateUniqueUsername(string name)
  {
    var counter = 1;
    var username = $"{name}_{counter}";

    while (await IsUsernameTaken(username))
    {
      counter++;
      username = $"{name}_{counter}";
    }

    return username;

  }

  public async Task<bool> IsUsernameTaken(string username) {

    var user = await _userManager.FindByNameAsync(username);

    return user is not null;
  }
  
  public string RemoveDiacritics(string text)
  {
    if (string.IsNullOrWhiteSpace(text))
      return string.Empty;

    var normalizedString = text.Normalize(NormalizationForm.FormD);
    var stringBuilder = new StringBuilder();

    foreach (var c in normalizedString)
    {
      var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
      // Skip non-spacing marks (diacritics/accents)
      if (unicodeCategory != UnicodeCategory.NonSpacingMark)
      {
        stringBuilder.Append(c);
      }
    }

    // Normalize back to composed form
    return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
  }
  
  public string CleanString(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
      return string.Empty;

   var text = name.ToLowerInvariant().Replace(" ", "");
    // First remove diacritics
    string withoutDiacritics = RemoveDiacritics(text);

    // Then remove special characters, keeping only alphanumeric and spaces
    string cleaned = MyRegex().Replace(withoutDiacritics, "");

    // Clean up multiple spaces
    cleaned = MyRegex1().Replace(cleaned, " ").Trim();

    return cleaned;
  }

  [GeneratedRegex(@"[^a-zA-Z0-9\s]")]
  private static partial Regex MyRegex();
  [GeneratedRegex(@"\s+")]
  private static partial Regex MyRegex1();
}