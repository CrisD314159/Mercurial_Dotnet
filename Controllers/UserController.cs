using System.Threading.Tasks;
using MercurialBackendDotnet.Dto.InputDTO;
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
}