using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesImplementations;


public class GetUserTopicsUseCase(UserManager<User> userManager, ITopicRepository topicRepository): IGetUserTopicsUseCase
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly ITopicRepository _topicRepository = topicRepository;
    public async Task<GetUserTopicsDTO> Execute(string userId, int offset, int limit)
  {
    var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");

    if (!await _userManager.IsEmailConfirmedAsync(user))
      throw new EntityValidationException("You're not verified");

    var topicsList = await _topicRepository.GetUserTopicsAsync(user.Id, offset, limit);

    return new GetUserTopicsDTO(topicsList);
  }
}