using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

public class TopicRepositoryImpl(MercurialDBContext mercurialDBContext):ITopicRepository
{
    private readonly MercurialDBContext _dbContext = mercurialDBContext;

    public async Task CreateTopicAsync(Topic topic)
    {
        await _dbContext.Topics.AddAsync(topic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTopicAsync(Topic topic)
    {
        _dbContext.Topics.Remove(topic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTopicAsync(Topic topic)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Topic> GetTopicByIdAsync(string topicId)
    {
        var topic = await _dbContext.Topics.FindAsync(topicId)
            ?? throw new EntityNotFoundException("Topic not found");
        return topic;
    }

    public async Task<List<TopicDTO>> GetUserTopicsAsync(string userId, int offset, int limit)
    {
        var userTopics = await _dbContext.Topics
        .Where(t => t.UserId == userId)
        .Select(t => new TopicDTO(t.Id, t.Title, t.Color, t.LastUpdatedAt))
        .Skip(offset)
        .Take(limit)
        .ToListAsync();

        return userTopics;
    }

    public async Task<bool> UserHasExceededTopicLimit(string userId)
    {
        var userTopics = await _dbContext.Topics
            .Where(t => t.UserId == userId).CountAsync();

        return userTopics >= 15;
    }

    public async Task<Topic> GetTopicByIdAndUserId(long topicId, string userId)
    {
        var topic = await _dbContext.Topics
        .Where(t => t.Id == topicId && t.UserId == userId)
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Topic not found");

        return topic;
    }

    public async Task<Topic> GetTopicByTopicNameAndUserId(string topicName, string userId)
    {
        var topic = await _dbContext.Topics
        .Where(t => t.Title == topicName && t.UserId == userId)
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Topic not found");

        return topic;
    }
}