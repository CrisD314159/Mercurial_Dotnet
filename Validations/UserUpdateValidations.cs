using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;


public class UserUpdateValidations: AbstractValidator<UpdateUserDTO>
{
  public UserUpdateValidations()
  {
    RuleFor(u => u.Name).MaximumLength(100).NotEmpty();
  }
}