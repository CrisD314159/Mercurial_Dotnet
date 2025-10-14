using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;

public class UpdateAssignmentValidations : AbstractValidator<UpdateAssignmentDTO>
{
  public UpdateAssignmentValidations()
  {
    RuleFor(a => a.Title).MaximumLength(100);
    RuleFor(a => a.NoteContent).MaximumLength(500);
  }
}