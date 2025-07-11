using System.Security.Claims;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IAccountService accountService, SignInManager<User> signInManager, IConfiguration configuration) : ControllerBase
{

  private readonly IAccountService _accountService = accountService;
  private readonly SignInManager<User> _signInManager = signInManager;
  private readonly IConfiguration _configuration = configuration;

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginDTO loginDTO)
  {
    var loginResponse = await _accountService.Login(loginDTO);
    return Ok(loginResponse);
  }

  [HttpDelete("logout")]
  public async Task<IActionResult> Logout(RefreshTokenDTO refreshTokenDTO)
  {
    await _accountService.Logout(refreshTokenDTO.RefreshToken);
    return Ok();
  }

  [HttpPut("refreshToken")]
  public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshTokenDTO)
  {
    var refreshResponse = await _accountService.RefreshToken(refreshTokenDTO.RefreshToken);
    return Ok(refreshResponse);
  }

  /// <summary>
  /// This is the endpoint where the client shoul call.
  /// This mehod triggers or redirects the user to google auth service
  /// </summary>
  /// <param name="returnUrl"></param>
  /// <returns></returns>
  [HttpGet("api/login/google")]
  public IActionResult GoogleSignIn([FromQuery] string returnUrl="/")
  {
    var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google",
    Url.Action("ExternalLoginCallback", "Account", new { returnUrl }));

    return Challenge(properties, "Google");
  }

  /// <summary>
  /// After a success login, google redirects to this method
  /// Here we extract the user data to return a JWT for user access
  /// This method calls SignInUsingGoogle who handles the jwt creation
  /// </summary>
  /// <param name="returnUrl"></param>
  /// <returns></returns>
  [HttpGet("api/login/google/callback")]
  public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
  {
    var userInfo = await _signInManager.GetExternalLoginInfoAsync();
    var frontUrl = _configuration["Api:Url"];

    if (userInfo == null)
    {
      return Redirect($"{frontUrl}/login?error=login_failed");
    }

    var email = userInfo.Principal.FindFirstValue(ClaimTypes.Email);
    var name = userInfo.Principal.FindFirstValue(ClaimTypes.Name);

    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
    {
      return Redirect($"{frontUrl}/login?error=login_failed");
    }

    var tokenSession = await _accountService.SignInUsingGoogle(email, name);

    return Redirect($"{frontUrl}/api/auth/google?token={tokenSession.Token}&refresh={tokenSession.RefreshToken}");
  }


}