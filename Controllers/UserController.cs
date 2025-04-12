using System.Security.Claims;
using System.Threading.Tasks;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Services.Interfaces;
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
  public async Task<IActionResult> DeleteUser()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorize to perform this action");

    await _userService.DeleteUser(userId);
    return Ok();
  }

  [HttpGet]
  public async Task<IActionResult> GetUserOverview()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorize to perform this action");
    var user = await _userService.GetUserOverview(userId);
    return Ok(user);
  }

  [HttpPut]
  public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorize to perform this action");
    
    await _userService.UpdateUser(userId, updateUserDTO);
    return Ok();
  }

  [HttpPut ("verifyUser")]
  public async Task<IActionResult> VerifyUser(VerifyuserDTO verifyuserDTO)
  {
    await _userService.VerifyUser(verifyuserDTO);
    return Ok();

  }
}