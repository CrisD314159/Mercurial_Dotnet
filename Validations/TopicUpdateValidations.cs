using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;


public class TopicUpdateValidations: AbstractValidator<UpdateTopicDTO>
{
  public TopicUpdateValidations()
  {
    RuleFor(t => t.Title).MaximumLength(100);
    RuleFor(t => t.Color).MaximumLength(9);
  }
}