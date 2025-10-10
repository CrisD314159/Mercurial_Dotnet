using FluentValidation;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.Validations;


public class NodeUpdateValidations: AbstractValidator<UpdateNodeDTO>
{
  public NodeUpdateValidations()
  {
    RuleFor(n => n.Content).MaximumLength(105).NotEmpty();
    RuleFor(n => n.NodeId).NotEmpty();
  }
}