using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface ITopicRepository
{
  Task CreateTopicAsync(Topic topic);
  Task DeleteTopicAsync(Topic topic);
  Task UpdateTopicAsync(Topic topic);
  Task<Topic> GetTopicByIdAsync(string topicId);
  Task<List<TopicDTO>> GetUserTopicsAsync(string userId, int offset, int limit);
  Task<bool> UserHasExceededTopicLimit(string userId);
  Task<Topic> GetTopicByIdAndUserId(long topicId, string userId);
  Task<Topic> GetTopicByTopicNameAndUserId(string topicName, string userId);
  
}