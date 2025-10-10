using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

public class TopicRepositoryImpl:ITopicRepository
{
    public Task CreateTopicAsync(Topic topic)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Topic topic)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTopicAsync(Topic topic)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> GetTopicByIdAsync(string topicId)
    {
        throw new NotImplementedException();
    }

    public Task<List<TopicDTO>> GetUserTopicsAsync(string userId, int offset, int limit)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserHasExceededTopicLimit(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> GetTopicByIdAndUserId(long topicId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> GetTopicByTopicNameAndUserId(string topicName, string userId)
    {
        throw new NotImplementedException();
    }
}