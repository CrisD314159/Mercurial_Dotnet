using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;


public class NodeValidations: AbstractValidator<AddNodeDTO>
{
  public NodeValidations()
  {
    RuleFor(n => n.Content).MaximumLength(105).NotEmpty();
    RuleFor(n => n.ListId).NotEmpty();
  }
}