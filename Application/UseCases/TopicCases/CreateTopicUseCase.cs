using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases;


public class CreateTopicUseCase(IValidator<CreateTopicDTO> validator,
 VerifyValidTopicService verifyTopic,
 UserManager<User> userManager, 
 ITopicRepository topicRepository)
{
  private readonly UserManager<User> _userManager = userManager;
  private readonly  ITopicRepository _topicRepository = topicRepository;

  private readonly IValidator<CreateTopicDTO> _validator = validator;
  private readonly VerifyValidTopicService _verifyTopic = verifyTopic;
  public async Task Execute(string userId, CreateTopicDTO createTopicDTO)
  {
    _validator.ValidateAndThrow(createTopicDTO);
    if (await _verifyTopic.Execute(createTopicDTO.Title, userId))
    {
      var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("User not found");
      if (!user.EmailConfirmed) throw new EntityValidationException("You are not verified");
      Topic topic = new()
      {
        Title = createTopicDTO.Title,
        Color = createTopicDTO.Color,
        LastUpdatedAt = DateOnly.FromDateTime(DateTime.Now),
        User = user
      };
      await _topicRepository.CreateTopicAsync(topic);
    }
  }
}