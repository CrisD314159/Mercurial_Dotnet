using MercurialBackendDotnet.Domain.Entities;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface ITopicRepository
{
  Task CreateTopicAsync(Topic topic);

  Task DeleteTopicId(string topicId);

  Task UpdateAssingmetAsync(Topic topic);

  Task<Topic> GetTopicByIdAync(string topicId);

  Task<IEnumerable<Topic>> GetUserTopicsAsync(string userId);
  
}