using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;

public class AssignmentValidations : AbstractValidator<CreateAssignmentDTO>
{
  public AssignmentValidations()
  {
    RuleFor(a => a.Title).MaximumLength(100);
    RuleFor(a => a.NoteContent).MaximumLength(500);
  }
}