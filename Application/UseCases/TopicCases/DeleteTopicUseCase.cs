using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases;


public class DeleteTopicUseCase(ITopicRepository topicRepository)
{

  private readonly ITopicRepository _topicRepository = topicRepository;
  public async Task Execute(string userId, long topicId)
  {
    var topic = await _topicRepository.GetTopicByIdAndUserId(topicId, userId)
    ?? throw new EntityNotFoundException("Topic not found");

    await _topicRepository.DeleteTopicAsync(topic);
  }

}