using System.Security.Claims;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.SubjectCases;
using MercurialBackendDotnet.Application.UseCases.UserCases;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectController(
  CreateSubjectUseCase createSubjectUseCase,
  DeleteSubjectUseCase deleteSubjectUseCase,
  GetUserSubjectsUseCase getUserSubjectsUseCase,
  UpdateSubjectUseCase updateSubjectUseCase
  ) : ControllerBase
{
  private readonly CreateSubjectUseCase _createSubjectUseCase = createSubjectUseCase;
  private readonly DeleteSubjectUseCase _deleteSubjectUseCase = deleteSubjectUseCase;
  private readonly GetUserSubjectsUseCase _getUserSubjectsUseCase = getUserSubjectsUseCase;
  private readonly UpdateSubjectUseCase _updateSubjectUseCase = updateSubjectUseCase;


  [HttpPost]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateSubject(CreateSubjectDTO createSubjectDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _createSubjectUseCase.Execute(userId, createSubjectDTO);
    return Created();
  }

  [HttpPut]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateSubject(UpdateSubjectDTO updateSubjectDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _updateSubjectUseCase.Execute(userId, updateSubjectDTO);
    return Ok();
  }

  [HttpDelete("{subjectId:long}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteSubject(long subjectId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    await _deleteSubjectUseCase.Execute(userId, subjectId);
    return Ok();
  }

  [HttpGet]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetUserSubjects(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");
    var subjects = await _getUserSubjectsUseCase.Execute(userId, offset, limit);
    return Ok(subjects);
  }

}