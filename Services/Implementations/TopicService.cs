using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class TopicService(MercurialDBContext dBContext) : ITopicService
{

  private readonly MercurialDBContext _dbContext = dBContext;

  public async Task CreateTopic(Guid userId, CreateTopicDTO createTopicDTO)
  {
    TopicValidations validationRules = new();
    ValidationResult result = validationRules.Validate(createTopicDTO);
    if(!result.IsValid) throw new VerificationException(result.Errors);
    if(await VerifyValidTopic(createTopicDTO.Title, userId) )
    {
      Topic topic = new(){
        Title = createTopicDTO.Title,
        Color = createTopicDTO.Color,
        LastUpdatedAt = DateOnly.FromDateTime(DateTime.Now),
        User =  await _dbContext.Users.FindAsync(userId) ?? throw new EntityNotFoundException("User does not exists")
      };
      await _dbContext.Topics.AddAsync(topic);
      await _dbContext.SaveChangesAsync();
    }
  }

  private async Task<bool> VerifyValidTopic(string title, Guid userId)
  {
    if(_dbContext.Topics.Where(t => t.UserId == userId).Count() > 10) throw new VerificationException("You've reached your maximum ammount of topics");
    if(await _dbContext.Topics.AnyAsync(t => t.Title == title && t.UserId == userId)) throw new EntityAlreadyExistsException($"There's already a topic with title {title}");
    return true;
  }

  public async Task DeleteTopic(long topicId)
  {
    var topic = await _dbContext.Topics.FindAsync(topicId) ?? throw new EntityNotFoundException("Topic not found");
    _dbContext.Topics.Remove(topic);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<GetUserTopicsDTO> GetUserTopics(Guid userId, int offset, int limit)
  {
    var topicsList = await _dbContext.Topics.Select(t => new TopicDTO(
      t.Id,
      t.Title,
      t.Color,
      t.LastUpdatedAt
    )).Skip(offset).Take(limit).ToListAsync() ?? [];

    return new GetUserTopicsDTO(topicsList);  
  }

  public async Task UpdateTopic(UpdateTopicDTO updateTopicDTO)
  {
    TopicUpdateValidations validationRules = new();
    ValidationResult result = validationRules.Validate(updateTopicDTO);
    if(!result.IsValid) throw new VerificationException(result.Errors);

    var topic = await _dbContext.Topics.FindAsync(updateTopicDTO.TopicId) 
    ?? throw new EntityNotFoundException("Topic does not exists");

    topic.Color = updateTopicDTO.Color;
    topic.Title = updateTopicDTO.Title;

    await _dbContext.SaveChangesAsync();
    
  }
}