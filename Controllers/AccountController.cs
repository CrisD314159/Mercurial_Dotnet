using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{

  private readonly IAccountService _accountService = accountService;

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
}