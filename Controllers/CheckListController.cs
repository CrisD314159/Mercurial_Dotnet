using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckListController(ICheckListService checkListService):ControllerBase 
{

  private readonly ICheckListService _checklistService = checkListService;

  [HttpPost]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateChecklist(CreateChecklistDTO createChecklistDTO)
  {
    await _checklistService.CreateCheckList(createChecklistDTO.AssignmentId);
    return Created();
  }

  [HttpPost("addNode")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> AddNodeChecklist(AddNodeDTO addNodeDTO)
  {
    await _checklistService.AddNode(addNodeDTO);
    return Ok();
  }

  [HttpGet("{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetChecklist(Guid assignmentId)
  {
    var checkList = await _checklistService.GetChecklist(assignmentId);
    return Ok(checkList);
  }

  [HttpDelete("{checklistId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteChecklist(long checklistId)
  {
    await _checklistService.DeleteCheckList(checklistId);
    return Ok();
  }

  [HttpDelete("removeNode/{nodeId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> RemoveNodeFromChecklist(long nodeId)
  {
    await _checklistService.RemoveNode(nodeId);
    return Ok();
  }

  [HttpPatch("markAsDoneNode/{nodeId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MarkAsDoneNode(long nodeId)
  {
    await _checklistService.MarkAsDoneNode(nodeId);
    return Ok();
  }

  [HttpPatch("unmarkAsDoneNode/{nodeId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UnmarkAsDoneNode(long nodeId)
  {
    await _checklistService.UnmarkAsDoneNode(nodeId);
    return Ok();
  }

  [HttpPut]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateNode(UpdateNodeDTO nodeDTO)
  {
    await _checklistService.UpdateNode(nodeDTO);
    return Ok();
  }

}