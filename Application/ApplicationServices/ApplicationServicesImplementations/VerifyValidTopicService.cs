using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;

public class VerifyValidTopicService(ITopicRepository topicService): IVerifyValidTopicService
{

  private readonly ITopicRepository _topicService = topicService;
  public async Task<bool> Execute(string title, string userId)
  {
    Topic topic;
    if (await _topicService.UserHasExceededTopicLimit(userId))
      throw new ExceededLimitException("You've reached your maximum ammount of topics");

    try
    {
        topic = await _topicService.GetTopicByTopicNameAndUserId(title, userId);
    }
    catch (EntityNotFoundException)
    {
      return true;
    }
    if (topic != null)
      throw new EntityValidationException($"There's already a topic with title {title}");
    return true;
  }

}