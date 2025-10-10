using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;


public class UserUpdateValidations: AbstractValidator<UpdateUserDTO>
{
  public UserUpdateValidations()
  {
    RuleFor(u => u.Name).MaximumLength(100).NotEmpty();
  }
}