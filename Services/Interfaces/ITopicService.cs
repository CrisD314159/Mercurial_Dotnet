using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ITopicService
{
  Task CreateTopic(string userId, CreateTopicDTO createTopicDTO);

  Task UpdateTopic(string userId, UpdateTopicDTO updateTopicDTO);

  Task DeleteTopic(string userId, long topicId);

  Task<GetUserTopicsDTO> GetUserTopics(string userId, int offset, int limit);

  Task<bool> UserHasExceededTopicLimit(string userId);

  Task<Topic> GetTopicByIdAndUserId(string topicId, string userId);
  Task<Topic> GetTopicByTopicNameAndUserId(string topicName, string userId);
}