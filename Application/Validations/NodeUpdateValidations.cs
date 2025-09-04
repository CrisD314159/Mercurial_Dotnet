using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;


public class NodeUpdateValidations: AbstractValidator<UpdateNodeDTO>
{
  public NodeUpdateValidations()
  {
    RuleFor(n => n.Content).MaximumLength(105).NotEmpty();
    RuleFor(n => n.NodeId).NotEmpty();
  }
}