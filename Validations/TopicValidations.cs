using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;


public class TopicValidations: AbstractValidator<CreateTopicDTO>
{
  public TopicValidations()
  {
    RuleFor(t => t.Title).MaximumLength(100);
    RuleFor(t => t.Color).MaximumLength(9);
  }
}