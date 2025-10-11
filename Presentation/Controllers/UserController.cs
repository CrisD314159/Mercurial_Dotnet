using System.Security.Claims;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MercurialBackendDotnet.Application.UseCases.UserCases;
using MercurialBackendDotnet.Application.ApplicationExceptions;

namespace MercurialBackendDotnet.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
  ChangeUserPasswordUseCase changeUserPasswordUseCase,
  CreateRegularUserUseCase createRegularUserUseCase,
  CreateThirdPartyUserUseCase createThirdPartyUserUseCase,
  DeleteUserUseCase deleteUserUseCase,
  GetUserOverviewUseCase getUserOverviewUseCase,
  RecoverUserAccountUseCase recoverUserAccountUseCase,
  UpdateUserUseCase updateUserUseCase,
  VerifyUserUseCase verifyUserUseCase

  ) : ControllerBase
{
  private readonly ChangeUserPasswordUseCase _changeUserPasswordUseCase = changeUserPasswordUseCase;
  private readonly CreateRegularUserUseCase _createRegularUserUseCase = createRegularUserUseCase;
  private readonly CreateThirdPartyUserUseCase _createThirdPartyUserUseCase = createThirdPartyUserUseCase;
  private readonly DeleteUserUseCase _deleteUserUseCase = deleteUserUseCase;
  private readonly GetUserOverviewUseCase _getUserOverviewUseCase = getUserOverviewUseCase;
  private readonly RecoverUserAccountUseCase _recoverUserAccountUseCase = recoverUserAccountUseCase;
  private readonly UpdateUserUseCase _updateUserUseCase = updateUserUseCase;
  private readonly VerifyUserUseCase _verifyUserUseCase = verifyUserUseCase;


  [HttpPost]
  public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
  {
    await _createRegularUserUseCase.Execute(createUserDTO);
    return Created();
  }


  [HttpDelete]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteUser()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorized to perform this action");

    await _deleteUserUseCase.Execute(userId);
    return Ok();
  }

  [HttpGet]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetUserOverview()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorized to perform this action");
    var user = await _getUserOverviewUseCase.Execute(userId);
    return Ok(user);
  }

  [HttpPut]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
    throw new UnauthorizedException("You're not authorized to perform this action");
    
    await _updateUserUseCase.Execute(userId, updateUserDTO);
    return Ok();
  }

  [HttpPut("verifyUser")]
  public async Task<IActionResult> VerifyUser(VerifyuserDTO verifyuserDTO)
  {
    await _verifyUserUseCase.Execute(verifyuserDTO);
    return Ok();

  }

  [HttpPut("recoverAccount")]
  public async Task<IActionResult> RecoverAccount(RecoverAccountDTO recoverAccountDTO)
  {
    await _recoverUserAccountUseCase.Execute(recoverAccountDTO);
    return Ok();
  }

  [HttpPut("changePassword")]
  public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
  {
    await _changeUserPasswordUseCase.Execute(changePasswordDTO);
    return Ok();
  }
}