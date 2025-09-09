using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MercurialBackendDotnet.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.ApplicationServices;

public partial class ValidateAndGenerateValidUsernameService(UserManager<User> userManager)
{

  private readonly UserManager<User> _userManager = userManager;
  public async Task<string> Execute(string currentUsername)
  {
    var cleannedUserName = CleanString(currentUsername);

    var uniqueUsername = await GenerateUniqueUsername(cleannedUserName);

    return uniqueUsername;
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
  


  [GeneratedRegex(@"[^a-zA-Z0-9\s]")]
  private static partial Regex MyRegex();
  [GeneratedRegex(@"\s+")]
  private static partial Regex MyRegex1();
  
}