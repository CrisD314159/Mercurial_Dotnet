using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;

public class SubjectUpdateValidations : AbstractValidator<UpdateSubjectDTO>
{
  public SubjectUpdateValidations()
  {
    RuleFor(s => s.Title).MaximumLength(100);
  }
}