using FluentValidation;
using MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesImplementations;


public class UpdateTopicUseCase(IValidator<UpdateTopicDTO> validator, ITopicRepository topicRepository): IUpdateTopicUseCase
{

  private readonly IValidator<UpdateTopicDTO> _validator = validator;
  private readonly ITopicRepository _topicRepository = topicRepository;
  public async Task Execute(string userId, UpdateTopicDTO updateTopicDTO)
  {
    _validator.ValidateAndThrow(updateTopicDTO);

    var topic = await _topicRepository.GetTopicByIdAndUserId(updateTopicDTO.TopicId, userId)
    ?? throw new EntityNotFoundException("Topic not found"); ;

    topic.Color = updateTopicDTO.Color;
    topic.Title = updateTopicDTO.Title;
    topic.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _topicRepository.UpdateTopicAsync(topic);
  }
}