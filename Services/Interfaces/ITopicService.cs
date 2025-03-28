using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ITopicService
{
  void CreateTopic(CreateTopicDTO createTopicDTO);

  void UpdateTopic(UpdateTopicDTO updateTopicDTO);

  void DeleteTopic(string topicId);

  GetUserTopicsDTO GetUserTopics(string userId, int offset, int limit);
}