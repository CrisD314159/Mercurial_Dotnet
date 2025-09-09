using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases;


public class UpdateUser(UserManager<User> userManager, IValidator<UpdateUserDTO> validator)
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly IValidator<UpdateUserDTO> _validator = validator;
  public async Task Execute(string id, UpdateUserDTO updateUserDTO)
  {
    _validator.ValidateAndThrow(updateUserDTO);
    var user = await _userManager.FindByIdAsync(id) ?? throw new EntityNotFoundException("User does not exist");

    user.Name = updateUserDTO.Name;
    user.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      throw new InternalServerException("Cannot update user");
    }
  }
  
}