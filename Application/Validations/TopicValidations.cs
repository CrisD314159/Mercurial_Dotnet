using FluentValidation;
using MercurialBackendDotnet.Dto.InputDTO;

namespace MercurialBackendDotnet.Validations;


public class TopicValidations: AbstractValidator<CreateTopicDTO>
{
  public TopicValidations()
  {
    RuleFor(t => t.Title).MaximumLength(70).NotEmpty();
    RuleFor(t => t.Color).MaximumLength(9).NotEmpty();
  }
}