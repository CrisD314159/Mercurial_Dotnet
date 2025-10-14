using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;
public class SubjectValidations : AbstractValidator<CreateSubjectDTO>
{
  public SubjectValidations()
  {
    RuleFor(s => s.Title).MaximumLength(70);
  }
}