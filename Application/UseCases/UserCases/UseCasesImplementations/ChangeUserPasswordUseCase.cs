using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesImplementations;


public class ChangeUserPasswordUseCase(IValidator<ChangePasswordDTO> validator, UserManager<User> userManager):IChangeUserPasswordUseCase
{
  private readonly IValidator<ChangePasswordDTO> _validator = validator;
  private readonly UserManager<User> _userManager = userManager;
  public async Task Execute(ChangePasswordDTO changePasswordDTO)
  {
    _validator.ValidateAndThrow(changePasswordDTO);

    var user = await _userManager.FindByEmailAsync(changePasswordDTO.Email)
    ?? throw new EntityNotFoundException("User not found");


    var result = await _userManager.ResetPasswordAsync(user, changePasswordDTO.Code, changePasswordDTO.Password);

    if (!result.Succeeded)
    {
      throw new InternalServerException(string.Join("; ", result.Errors.Select(e => e.Description)));
    }
  }
}