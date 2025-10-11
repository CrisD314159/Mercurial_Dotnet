using System.Runtime.CompilerServices;
using System.Security.Claims;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MercurialBackendDotnet.Application.UseCases.AssignmentCases;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;

namespace MercurialBackendDotnet.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AssignmentController(
  ICreateAssignmentUseCase createAssignmentUseCase,
  IDeleteAssignmentUseCase deleteAssignmentUseCase,
  IGetUserDoneAssignmentsUseCase getUserDoneAssignmentsUseCase,
  IGetUserTodoAssignmentsUseCase getUserTodoAssignmentsUseCase,
  IMarkAssignmentAsDoneUseCase markAssignmentAsDoneUseCase,
  IMarkAssignmentInProgressUseCase markAssignmentInProgressUseCase,
  IMarkAssignmentTodoUseCase markAssignmentTodoUseCase,
  IUpdateAssignmentUseCase updateAssignmentUseCase
) : ControllerBase
{
  private readonly ICreateAssignmentUseCase _createAssignmentUseCase = createAssignmentUseCase;
  private readonly IDeleteAssignmentUseCase _DeleteAssignmentUseCase= deleteAssignmentUseCase;
  private readonly IGetUserDoneAssignmentsUseCase _getUserDoneAssignmentsUseCase = getUserDoneAssignmentsUseCase;
  private readonly IGetUserTodoAssignmentsUseCase _getUserTodoAssignmentsUseCase = getUserTodoAssignmentsUseCase;
  private readonly IMarkAssignmentAsDoneUseCase _markAssignmentAsDoneUseCase = markAssignmentAsDoneUseCase;
  private readonly IMarkAssignmentInProgressUseCase _markAssignmentInProgressUseCase = markAssignmentInProgressUseCase;
  private readonly IMarkAssignmentTodoUseCase _markAssignmentTodoUseCase = markAssignmentTodoUseCase;
  private readonly IUpdateAssignmentUseCase _updateAssignmentUseCase = updateAssignmentUseCase;

  [HttpPost]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateAssignment(CreateAssignmentDTO createAssignmentDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _createAssignmentUseCase.Execute(userId, createAssignmentDTO);
    return Created();

  }

  [HttpPut]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateAssignment(UpdateAssignmentDTO updateAssignmentDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _updateAssignmentUseCase.Execute(userId, updateAssignmentDTO);
    return Ok();

  }

  [HttpDelete("{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteAssignmet(Guid assignmentId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _DeleteAssignmentUseCase.Execute(userId, assignmentId);
    return Ok();

  }

  [HttpGet("doneAssignments")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetDoneAssignment(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    var assignments = await _getUserDoneAssignmentsUseCase.Execute(userId, offset, limit);
    return Ok(assignments);

  }

  [HttpGet("todoAssignments")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetTodoAssignment(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    var assignments = await _getUserTodoAssignmentsUseCase.Execute(userId, offset, limit);
    return Ok(assignments);

  }

  [HttpPatch("markAsDone/{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MarkAsDoneAssignment(Guid assignmentId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _markAssignmentAsDoneUseCase.Execute(userId, assignmentId);
    return Ok();

  }
  [HttpPatch("markAsTodo/{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MarkAsTodoAssignment(Guid assignmentId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _markAssignmentTodoUseCase.Execute(userId, assignmentId);
    return Ok();

  }


}