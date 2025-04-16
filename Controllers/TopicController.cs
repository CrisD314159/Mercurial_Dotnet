using System.Runtime.CompilerServices;
using System.Security.Claims;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class TopicController(ITopicService topicService) : ControllerBase
{

  private readonly ITopicService _topicService = topicService;


  [HttpPost]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateTopic(CreateTopicDTO createTopicDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _topicService.CreateTopic(userId, createTopicDTO);
    return Created();

  }
  
  [HttpPut]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateTopic(UpdateTopicDTO updateTopicDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _topicService.UpdateTopic(userId, updateTopicDTO);
    return Ok();
  }
  

  [HttpDelete("{topicId:long}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteTopic(long topicId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _topicService.DeleteTopic(userId, topicId);
    return Ok();
  }

  [HttpGet]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetTopics(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    var topics = await _topicService.GetUserTopics(userId, offset, limit);
    return Ok(topics);
  }
}