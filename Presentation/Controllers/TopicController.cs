using System.Runtime.CompilerServices;
using System.Security.Claims;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MercurialBackendDotnet.Application.UseCases.TopicCases;
using MercurialBackendDotnet.Application.ApplicationExceptions;

namespace MercurialBackendDotnet.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TopicController(
  CreateTopicUseCase createTopicUseCase,

DeleteTopicUseCase deleteTopicUseCase,
GetUserTopicsUseCase getUserTopicsUseCase,
UpdateTopicUseCase updateTopicUseCase
  ) : ControllerBase
{

  private readonly CreateTopicUseCase _createTopicUseCase = createTopicUseCase;
  private readonly DeleteTopicUseCase _deleteTopicUseCase = deleteTopicUseCase;
  private readonly GetUserTopicsUseCase _getUserTopicsUseCase = getUserTopicsUseCase;
  private readonly UpdateTopicUseCase _updateTopicUseCase = updateTopicUseCase;


  [HttpPost]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateTopic(CreateTopicDTO createTopicDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _createTopicUseCase.Execute(userId, createTopicDTO);
    return Created();

  }
  
  [HttpPut]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateTopic(UpdateTopicDTO updateTopicDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _updateTopicUseCase.Execute(userId, updateTopicDTO);
    return Ok();
  }
  

  [HttpDelete("{topicId:long}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteTopic(long topicId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _deleteTopicUseCase.Execute(userId, topicId);
    return Ok();
  }

  [HttpGet]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetTopics(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    var topics = await _getUserTopicsUseCase.Execute(userId, offset, limit);
    return Ok(topics);
  }
}