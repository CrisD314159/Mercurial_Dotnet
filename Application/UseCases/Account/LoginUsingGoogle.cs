using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.Account;


public class LoginUsingGoogle(UserManager<User> userManager, GenerateThirdPartyTokenService generateThirdPartyTokenService)
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly GenerateThirdPartyTokenService _generateThirdPartyTokenService = generateThirdPartyTokenService;

  public async Task<LoginResponseDTO> Execute(string email, string name)
  {
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null)
    {
      var newUser = await _thirdPartyAccountService.CreateThirdPartyUser(email, name);
      return await _generateThirdPartyTokenService.Execute(newUser.Id, newUser.Email ?? "");
    }

    return await _generateThirdPartyTokenService.Execute(user.Id, user.Email!);


  }
  
}