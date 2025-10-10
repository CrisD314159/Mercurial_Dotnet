using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Application.UseCases.UserCases;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.Account;


public class LoginUsingGoogleUseCase(UserManager<User> userManager,
GenerateThirdPartyTokenService generateThirdPartyTokenService,
CreateThirdPartyUserUseCase createThirdPartyUserUseCase
)
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly GenerateThirdPartyTokenService _generateThirdPartyTokenService = generateThirdPartyTokenService;
  private readonly CreateThirdPartyUserUseCase _createThirdPartyUserUseCase = createThirdPartyUserUseCase;

  public async Task<LoginResponseDTO> Execute(string email, string name)
  {
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null)
    {
      var newUser = await _createThirdPartyUserUseCase.Execute(email, name);
      return await _generateThirdPartyTokenService.Execute(newUser.Id, newUser.Email ?? "");
    }

    return await _generateThirdPartyTokenService.Execute(user.Id, user.Email!);


  }
  
}