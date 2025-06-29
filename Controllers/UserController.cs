using System.Security.Claims;
using System.Threading.Tasks;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
  private readonly IUserService _userService = userService;


  [HttpPost]
  public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
  {
    await _userService.CreateUser(createUserDTO);
    return Created();
  }


  [HttpDelete]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteUser()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorized to perform this action");

    await _userService.DeleteUser(userId);
    return Ok();
  }

  [HttpGet]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetUserOverview()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorized to perform this action");
    var user = await _userService.GetUserOverview(userId);
    return Ok(user);
  }

  [HttpPut]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorized to perform this action");
    
    await _userService.UpdateUser(userId, updateUserDTO);
    return Ok();
  }

  [HttpPut("verifyUser")]
  public async Task<IActionResult> VerifyUser(VerifyuserDTO verifyuserDTO)
  {
    await _userService.VerifyUser(verifyuserDTO);
    return Ok();

  }

  [HttpPut("recoverAccount")]
  public async Task<IActionResult> RecoverAccount(RecoverAccountDTO recoverAccountDTO)
  {
    await _userService.RecoverAccount(recoverAccountDTO);
    return Ok();
  }

  [HttpPut("changePassword")]
  public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
  {
    await _userService.ChangePassword(changePasswordDTO);
    return Ok();
  }
}