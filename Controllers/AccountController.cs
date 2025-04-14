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




}