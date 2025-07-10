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

  [HttpGet("api/login/google")]
  public IActionResult GoogleSignIn([FromQuery] string returnUrl="/")
  {
    var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google",
    Url.Action("ExternalLoginCallback", "Account", new { returnUrl }));

    return Challenge(properties, "Google");
  }

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