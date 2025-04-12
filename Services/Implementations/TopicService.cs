using FluentValidation;
using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class TopicService(MercurialDBContext dBContext, IValidator<CreateTopicDTO> validatorCreate,
 IValidator<UpdateTopicDTO> validatorUpdate, UserManager<User> userManager
) : ITopicService
{

  private readonly MercurialDBContext _dbContext = dBContext;
  private readonly IValidator<CreateTopicDTO> _validatorCreate = validatorCreate;
  private readonly IValidator<UpdateTopicDTO> _validatorUpdate = validatorUpdate;
  private readonly UserManager<User> _userManager = userManager;

  /// <summary>
  /// Creates a topic
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="createTopicDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task CreateTopic(string userId, CreateTopicDTO createTopicDTO)
  {
    _validatorCreate.ValidateAndThrow(createTopicDTO);
    if(await VerifyValidTopic(createTopicDTO.Title, userId) )
    {
      Topic topic = new(){
        Title = createTopicDTO.Title,
        Color = createTopicDTO.Color,
        LastUpdatedAt = DateOnly.FromDateTime(DateTime.Now),
        User =  await _dbContext.Users.FindAsync(userId) ?? throw new EntityNotFoundException("User not found")
      };
      await _dbContext.Topics.AddAsync(topic);
      await _dbContext.SaveChangesAsync();
    }
  }

  /// <summary>
  /// Verifies if a topic already exists in the user account
  /// Besides, this method also verifies if user alreade hav reached his maximum
  /// ammount of topics
  /// </summary>
  /// <param name="title"></param>
  /// <param name="userId"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityAlreadyExistsException"></exception>
  private async Task<bool> VerifyValidTopic(string title, string userId)
  {
    if(_dbContext.Topics.Where(t => t.UserId == userId).Count() > 10) 
    throw new ExceededLimitException("You've reached your maximum ammount of topics");
    if(await _dbContext.Topics.AnyAsync(t => t.Title == title && t.UserId == userId)) 
    throw new EntityAlreadyExistsException($"There's already a topic with title {title}");
    return true;
  }

  /// <summary>
  /// Deletes permanetly a topic
  /// </summary>
  /// <param name="topicId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task DeleteTopic(long topicId)
  {
    var topic = await _dbContext.Topics.FindAsync(topicId) ?? throw new EntityNotFoundException("Topic not found");
    _dbContext.Topics.Remove(topic);
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Gets all the available user topics
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="offset"></param>
  /// <param name="limit"></param>
  /// <returns></returns>
  public async Task<GetUserTopicsDTO> GetUserTopics(string userId, int offset, int limit)
  {
    var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");

    if(!await _userManager.IsEmailConfirmedAsync(user)) throw new UnauthorizedException("You're not verified");

    var topicsList = await _dbContext.Topics.Where(t => t.UserId == userId).Select(t => new TopicDTO(
      t.Id,
      t.Title,
      t.Color,
      t.LastUpdatedAt
    )).Skip(offset).Take(limit).ToListAsync() ?? [];

    return new GetUserTopicsDTO(topicsList);  
  }

  /// <summary>
  /// Updates an existing topic (Title, color)
  /// </summary>
  /// <param name="updateTopicDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task UpdateTopic(UpdateTopicDTO updateTopicDTO)
  {
    _validatorUpdate.ValidateAndThrow(updateTopicDTO);

    var topic = await _dbContext.Topics.FindAsync(updateTopicDTO.TopicId) 
    ?? throw new EntityNotFoundException("Topic does not exists");

    topic.Color = updateTopicDTO.Color;
    topic.Title = updateTopicDTO.Title;
    topic.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _dbContext.SaveChangesAsync();
  }
}