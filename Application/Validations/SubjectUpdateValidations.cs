using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;

public class SubjectUpdateValidations : AbstractValidator<UpdateSubjectDTO>
{
  public SubjectUpdateValidations()
  {
    RuleFor(s => s.Title).MaximumLength(100);
  }
}