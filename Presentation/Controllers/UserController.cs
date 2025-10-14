using System.Security.Claims;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MercurialBackendDotnet.Application.UseCases.UserCases;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

namespace MercurialBackendDotnet.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
  IChangeUserPasswordUseCase changeUserPasswordUseCase,
  ICreateRegularUserUseCase createRegularUserUseCase,
  ICreateThirdPartyUserUseCase createThirdPartyUserUseCase,
  IDeleteUserUseCase deleteUserUseCase,
  IGetUserOverviewUseCase getUserOverviewUseCase,
  IRecoverUserAccountUseCase recoverUserAccountUseCase,
  IUpdateUserUseCase updateUserUseCase,
  IVerifyUserUseCase verifyUserUseCase

  ) : ControllerBase
{
  private readonly IChangeUserPasswordUseCase _changeUserPasswordUseCase = changeUserPasswordUseCase;
  private readonly ICreateRegularUserUseCase _createRegularUserUseCase = createRegularUserUseCase;
  private readonly ICreateThirdPartyUserUseCase _createThirdPartyUserUseCase = createThirdPartyUserUseCase;
  private readonly IDeleteUserUseCase _deleteUserUseCase = deleteUserUseCase;
  private readonly IGetUserOverviewUseCase _getUserOverviewUseCase = getUserOverviewUseCase;
  private readonly IRecoverUserAccountUseCase _recoverUserAccountUseCase = recoverUserAccountUseCase;
  private readonly IUpdateUserUseCase _updateUserUseCase = updateUserUseCase;
  private readonly IVerifyUserUseCase _verifyUserUseCase = verifyUserUseCase;


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