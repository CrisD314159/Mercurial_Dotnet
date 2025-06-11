using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Model.Enums;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Utils;
using MercurialBackendDotnet.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class UserService(MercurialDBContext dBContext, IAccountService accountService
, IValidator<CreateUserDTO> validator, IValidator<UpdateUserDTO> validatorUpdate, UserManager<User> userManager,
SignInManager<User> signInManager, IValidator<ChangePasswordDTO> changePasswordValidations
) : IUserService
{
  private readonly MercurialDBContext _dbContext = dBContext;

  private readonly IAccountService _accountService = accountService;

  private readonly IValidator<CreateUserDTO> _validator = validator;

  private readonly IValidator<UpdateUserDTO> _validatorUpdate = validatorUpdate;
  private readonly IValidator<ChangePasswordDTO> _changePasswordValidations = changePasswordValidations;

  private readonly UserManager<User> _userManager = userManager;

  private readonly SignInManager<User> _signInManager = signInManager;

  public async Task ChangePassword(ChangePasswordDTO changePasswordDTO)
  {
    _changePasswordValidations.ValidateAndThrow(changePasswordDTO);

    var user = await _userManager.FindByEmailAsync(changePasswordDTO.Email)
    ?? throw new EntityNotFoundException("User not found");


    var result = await _userManager.ResetPasswordAsync(user, changePasswordDTO.Code, changePasswordDTO.Password);

    if (!result.Succeeded)
    {
      throw new InternalServerException(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

  }

  /// <summary>
  /// Creates a new user
  /// </summary>
  /// <param name="createUserDTO"></param>
  /// <returns></returns>
  /// <exception cref="EntityAlreadyExistsException"></exception>
  /// <exception cref="VerificationException"></exception>
  public async Task CreateUser(CreateUserDTO createUserDTO)
  {
    if(await _userManager.FindByEmailAsync(createUserDTO.Email) != null)
    throw new EntityAlreadyExistsException("User already exists");

    _validator.ValidateAndThrow(createUserDTO);

    User user = new (){
      Id= Guid.NewGuid().ToString(),
      Name = createUserDTO.Name,
      State = UserState.NOT_VERIFIED,
      ProfilePicture = $"https://api.dicebear.com/9.x/thumbs/svg?seed={createUserDTO.Name}",
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
      VerificationCode = new Random().Next(1000, 9999).ToString(),
      UserName = createUserDTO.Name.Trim(),
      Email = createUserDTO.Email,
      EmailConfirmed = false
    };


    var result = await _userManager.CreateAsync(user, createUserDTO.Password);
    if(!result.Succeeded){
      throw new InternalServerException(string.Join(";", result.Errors.Select(e=>e.Description)));
    }
    await _accountService.SendAccountCreatedVerificationCode(createUserDTO.Name, createUserDTO.Email, user.VerificationCode);

  }

  /// <summary>
  /// Deletes a user using it's id
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task DeleteUser(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");
    var result = await _userManager.DeleteAsync(user);
    if (!result.Succeeded)
    {
      throw new InternalServerException("An error occurred while trying to delete user");
    }
  } 

  /// <summary>
  /// Returns a DTO with user basic ifo (id, email and profile picture)
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task<GetUserDTO> GetUserOverview(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("User does not exists");

    return new GetUserDTO(
      user.Id,
      user.Email ?? throw new EntityNotFoundException("Email not found"),
      user.ProfilePicture,
      user.Name
    );
  }

  public async Task RecoverAccount(RecoverAccountDTO recoverAccountDTO)
  {
    var user = await _userManager.FindByEmailAsync(recoverAccountDTO.Email)
    ?? throw new EntityNotFoundException("User does not exists");

    var recoveryCode = await _userManager.GeneratePasswordResetTokenAsync(user);

    await _accountService.SendRecoverAccountVerificationCode(user.Name, recoverAccountDTO.Email, recoveryCode);
  }

  /// <summary>
  /// Updates the info from a user (name)
  /// </summary>
  /// <param name="id"></param>
  /// <param name="updateUserDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task UpdateUser(string id, UpdateUserDTO updateUserDTO)
  {
    _validatorUpdate.ValidateAndThrow(updateUserDTO);
    var user = await _userManager.FindByIdAsync(id) ?? throw new EntityNotFoundException("User does not exist");

    user.Name = updateUserDTO.Name;
    user.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    var result = await _userManager.UpdateAsync(user);
    if(!result.Succeeded){
      throw new InternalServerException("Cannot update user");
    }
  }

  /// <summary>
  /// Verifies the user account after sign up
  /// </summary>
  /// <param name="verifyuserDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task VerifyUser(VerifyuserDTO verifyuserDTO)
  {
    var user = await _userManager.FindByEmailAsync(verifyuserDTO.Email)
    ?? throw new EntityNotFoundException("User does not exists");

    if (await _userManager.IsEmailConfirmedAsync(user)) throw new VerificationException("User is already verified");

    if (user.VerificationCode != verifyuserDTO.Code)
      throw new VerificationException("Invalid code or email");

    user.EmailConfirmed = true;
    user.State = UserState.ACTIVE;
    user.VerificationCode = "0";
    user.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      throw new InternalServerException("An error occurred while trying to verify your account");
    }
  }
}