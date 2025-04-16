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
public class SubjectController(ISubjectService subjectService) : ControllerBase
{
  private readonly ISubjectService _subjectService = subjectService;


  [HttpPost]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateSubject(CreateSubjectDTO createSubjectDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _subjectService.CreateSubjectDTO(userId, createSubjectDTO);
    return Created();
  }

  [HttpPut]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateSubject(UpdateSubjectDTO updateSubjectDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _subjectService.UpdateSubject(userId, updateSubjectDTO);
    return Ok();
  }

  [HttpDelete("{subjectId:long}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteSubject(long subjectId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _subjectService.DeleteSubject(userId, subjectId);
    return Ok();
  }

  [HttpGet]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetUserSubjects(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    var subjects = await _subjectService.GetUserSubjects(userId, offset, limit);
    return Ok(subjects);
  }

}