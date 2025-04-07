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
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class UserService(MercurialDBContext dBContext, IAccountService accountService
, IValidator<CreateUserDTO> validator, IValidator<UpdateUserDTO> validatorUpdate
) : IUserService
{
  private readonly MercurialDBContext _dbContext = dBContext;

  private readonly IAccountService _accountService = accountService;

  private readonly IValidator<CreateUserDTO> _validator = validator;

  private readonly IValidator<UpdateUserDTO> _validatorUpdate = validatorUpdate;

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
    throw new EntityAlreadyExistsException("User already exists");

    _validator.ValidateAndThrow(createUserDTO);

    var hashedPassword = PasswordManipulation.HashPassword(createUserDTO.Password);
    var verificationCode = new Random().Next(1000, 9999);

    User user = new (){
      Name = createUserDTO.Name,
      State = UserState.NOT_VERIFIED,
      ProfilePicture = $"https://api.dicebear.com/9.x/glass/svg?seed={createUserDTO.Name}",
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
      Account = new Account{Email = createUserDTO.Email, Password = hashedPassword, VerificationCode = verificationCode }
    };

    await _accountService.SendAccountCreatedVerificationCode(createUserDTO.Name, createUserDTO.Email, verificationCode);

    _dbContext.Users.Add(user);
    await _dbContext.SaveChangesAsync();

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
    _validatorUpdate.ValidateAndThrow(updateUserDTO);
    var user = await _dbContext.Users.FindAsync(id) ?? throw new EntityNotFoundException("User does not exist");

    user.Name = updateUserDTO.Name;
    user.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
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