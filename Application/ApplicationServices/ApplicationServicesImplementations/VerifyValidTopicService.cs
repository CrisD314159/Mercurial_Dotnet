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
    if (await _topicService.UserHasExceededTopicLimit(userId))
      throw new ExceededLimitException("You've reached your maximum ammount of topics");

    Topic topic = await _topicService.GetTopicByTopicNameAndUserId(title, userId);

    if (topic != null)
      throw new EntityAlreadyExistsException($"There's already a topic with title {title}");
    return true;
  }

}