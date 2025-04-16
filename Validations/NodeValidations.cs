using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;


public class NodeValidations: AbstractValidator<AddNodeDTO>
{
  public NodeValidations()
  {
    RuleFor(n => n.Content).MaximumLength(105).NotEmpty();
    RuleFor(n => n.ListId).NotEmpty();
  }
}