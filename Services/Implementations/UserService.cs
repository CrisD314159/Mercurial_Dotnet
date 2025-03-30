using System.Threading.Tasks;
using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class UserService(MercurialDBContext dBContext) : IUserService
{
  private readonly MercurialDBContext _dbContext = dBContext;
  public async Task CreateUser(CreateUserDTO createUserDTO)
  {
    if(await _dbContext.Users.AnyAsync(u => u.Account.Email == createUserDTO.Email ))
    throw new EntityAlreadyExistsException("User Already Exists");

    UserValidations validationRules = new ();
    ValidationResult result = validationRules.Validate(createUserDTO);
    if(!result.IsValid) throw new VerificationException(result.Errors);


    

  }

  public Task DeleteUser(string userId)
  {
    throw new NotImplementedException();
  }

  public Task<GetUserDTO> GetUserOverview(string userId)
  {
    throw new NotImplementedException();
  }

  public Task UpdateUser(UpdateUserDTO updateUserDTO)
  {
    throw new NotImplementedException();
  }

  public Task VerifyUser(VerifyuserDTO verifyuserDTO)
  {
    throw new NotImplementedException();
  }
}