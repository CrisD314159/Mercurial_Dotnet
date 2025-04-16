using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ITopicService
{
  Task CreateTopic(string userId, CreateTopicDTO createTopicDTO);

  Task UpdateTopic(string userId, UpdateTopicDTO updateTopicDTO);

  Task DeleteTopic(string userId, long topicId);

  Task<GetUserTopicsDTO> GetUserTopics(string userId, int offset, int limit);
}