using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;

public class PasswordChangeValidations : AbstractValidator<ChangePasswordDTO>
{
  public PasswordChangeValidations()
  {
    RuleFor(p => p.Email).EmailAddress();
    RuleFor(u => u.Password).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
    .WithMessage("Password should contain at least 8 characters, 1 uppercase, 1 lowercase, 1 digit and 1 special character").NotEmpty();
    RuleFor(p => p.Code).MinimumLength(1);
  }
}