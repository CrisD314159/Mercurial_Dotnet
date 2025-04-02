using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;

public class SubjectValidations : AbstractValidator<CreateSubjectDTO>
{
  public SubjectValidations()
  {
    RuleFor(s => s.Title).MaximumLength(100);
  }
}