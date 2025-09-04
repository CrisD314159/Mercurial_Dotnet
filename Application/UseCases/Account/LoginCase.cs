using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.Account;

public class LoginCase(
IEmailService emailService,
UserManager<User> userManager,
SignInManager<User> signInManager,
IJWTService jWtService,
IConfiguration configuration,
GenerateSessionService generateSessionService
)
{

  private readonly IEmailService _emailService = emailService;
  private readonly UserManager<User> _userManager = userManager;
  private readonly SignInManager<User> _signInManager = signInManager;
  private readonly IJWTService _jwtService = jWtService;
  private readonly IConfiguration _configuration = configuration;
  private readonly GenerateSessionService _generateSessionService = generateSessionService;

  public async Task<LoginResponseDTO> Execute(LoginDTO loginDTO)
  {
    var user = await _userManager.FindByEmailAsync(loginDTO.Email)
 ?? throw new EntityNotFoundException("User not found");

    if (user.IsThirdPartyUser) throw new ValidationException("Use your Google account to log in");

    user.VerifyValidUser();

    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

    if (!result.Succeeded)
    {
      throw new ValidationException("Invalid email or password");
    }

    if (result.Succeeded && user.Email != null)
    {
      var token = _jwtService.GenerateToken(user.Id, user.Email, "", false);
      var refreshToken = await _generateSessionService.CreateSession(user.Id, user.Email);

      return new LoginResponseDTO(token, refreshToken);
    }

    throw new InternalServerException("Cannot Login");
  }
  

  
}