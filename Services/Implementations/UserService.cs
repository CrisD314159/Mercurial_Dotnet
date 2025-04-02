using System.Threading.Tasks;
using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Model.Enums;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class UserService(MercurialDBContext dBContext, IAccountService accountService) : IUserService
{
  private readonly MercurialDBContext _dbContext = dBContext;

  private readonly IAccountService _accountService = accountService;

  /// <summary>
  /// Creates a new user
  /// </summary>
  /// <param name="createUserDTO"></param>
  /// <returns></returns>
  /// <exception cref="EntityAlreadyExistsException"></exception>
  /// <exception cref="VerificationException"></exception>
  public async Task CreateUser(CreateUserDTO createUserDTO)
  {
    if(await _dbContext.Users.AnyAsync(u => u.Account.Email == createUserDTO.Email ))
    throw new EntityAlreadyExistsException("User Already Exists");

    UserValidations validationRules = new ();
    ValidationResult result = validationRules.Validate(createUserDTO);
    if(!result.IsValid) throw new VerificationException(result.Errors);

    /// TODO: Password hashing and user registration

  }

  /// <summary>
  /// Deletes a user using it's id
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task DeleteUser(Guid userId)
  {
    var user = await _dbContext.Users.FindAsync(userId) ?? throw new EntityNotFoundException("User does not exist");
    user.State = UserState.DELETED;
    await _dbContext.SaveChangesAsync();
  } 

  /// <summary>
  /// Returns a DTO with user basic ifo (id, email and profile picture)
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task<GetUserDTO> GetUserOverview(Guid userId)
  {
    var user = await _dbContext.Users.Include(u => u.Account).Where(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new EntityNotFoundException("User does not exists");

    return new GetUserDTO(
      user.Id,
      user.Account.Email,
      user.ProfilePicture
    );
  }

  /// <summary>
  /// Updates the info from a user (name)
  /// </summary>
  /// <param name="id"></param>
  /// <param name="updateUserDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task UpdateUser(Guid id, UpdateUserDTO updateUserDTO)
  {
    UserUpdateValidations validations = new();
    ValidationResult result = validations.Validate(updateUserDTO);
    if(!result.IsValid) throw new VerificationException(result.Errors);

    var user = await _dbContext.Users.FindAsync(id) ?? throw new EntityNotFoundException("User does not exist");

    user.Name = updateUserDTO.Name;
    await _dbContext.SaveChangesAsync();
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
    if(!await _accountService.VerifyCode(verifyuserDTO.Email, verifyuserDTO.Code))
    throw new VerificationException("Invalid code, please try again");

    var user = await _dbContext.Users.Include(u=> u.Account).Where(u=> u.Account.Email == verifyuserDTO.Email).FirstOrDefaultAsync()
    ?? throw new EntityNotFoundException("User does not exists");
    
    user.State = UserState.ACTIVE;
    await _dbContext.SaveChangesAsync();
  }
}