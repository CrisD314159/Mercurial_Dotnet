using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.UseCases.Account.UseCasesInterfaces;
using MercurialBackendDotnet.Application.UseCases.UserCases;
using MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.Account.UseCasesImplementations;


public class LoginUsingGoogleUseCase(UserManager<User> userManager,
IGenerateThirdPartyTokenService generateThirdPartyTokenService,
ICreateThirdPartyUserUseCase createThirdPartyUserUseCase
): ILoginUsingGoogleUseCase
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly IGenerateThirdPartyTokenService _generateThirdPartyTokenService = generateThirdPartyTokenService;
  private readonly ICreateThirdPartyUserUseCase _createThirdPartyUserUseCase = createThirdPartyUserUseCase;

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