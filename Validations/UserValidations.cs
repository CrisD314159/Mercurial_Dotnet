using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Model;

namespace MercurialBackendDotnet.Validations;

public class UserValidations : AbstractValidator<CreateUserDTO>
{
  public UserValidations()
  {
    RuleFor(u => u.Email).EmailAddress().WithMessage("Field should be a valid E-mail address");
    RuleFor(u => u.Name).MaximumLength(100);
    RuleFor(u => u.Password).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
    .WithMessage("Password should contain at least 8 characters, 1 uppercase, 1 lowercase, 1 digit and 1 special character");
  }
}