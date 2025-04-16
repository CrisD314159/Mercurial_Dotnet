using System.Runtime.CompilerServices;
using System.Security.Claims;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class AssignmentController(IAssignmentService assignmentService): ControllerBase
{
  private readonly IAssignmentService _assignmentService = assignmentService;

  [HttpPost]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateAssignment(CreateAssignmentDTO createAssignmentDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _assignmentService.CreateAssignment(userId, createAssignmentDTO);
    return Created();

  }

  [HttpPut]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateAssignment(UpdateAssignmentDTO updateAssignmentDTO)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _assignmentService.UpdateAssignment(userId, updateAssignmentDTO);
    return Ok();

  }

  [HttpDelete("{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteAssignmet(Guid assignmentId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _assignmentService.DeleteAssignment(userId, assignmentId);
    return Ok();

  }

  [HttpGet("doneAssignments")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetDoneAssignment(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    var assignments = await _assignmentService.GetUserDoneTasks(userId, offset, limit);
    return Ok(assignments);

  }

  [HttpGet("todoAssignments")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetTodoAssignment(int offset, int limit)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    var assignments = await _assignmentService.GetUserTodoTasks(userId, offset, limit);
    return Ok(assignments);

  }

  [HttpPatch("markAsDone/{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MarkAsDoneAssignment(Guid assignmentId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _assignmentService.MarkAssignmentAsDone(userId, assignmentId);
    return Ok();

  }
  [HttpPatch("markAsTodo/{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MarkAsTodoAssignment(Guid assignmentId)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
    ?? throw new UnauthorizedException("You're not authorized to perform this action");

    await _assignmentService.MarkAssigmentTodo(userId, assignmentId);
    return Ok();

  }


}