using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;

public class AssignmentValidations : AbstractValidator<CreateAssignmentDTO>
{
  public AssignmentValidations()
  {
    RuleFor(a => a.Title).MaximumLength(100);
    RuleFor(a => a.NoteContent).MaximumLength(500);
  }
}