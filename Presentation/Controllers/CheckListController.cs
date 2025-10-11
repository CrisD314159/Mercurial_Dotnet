using MercurialBackendDotnet.Application.UseCases.CheckListCases;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercurialBackendDotnet.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckListController(
  AddNodeUseCase addNodeUseCase,
  CreateChecklistUseCase createChecklistUseCase,
  DeleteChecklistUseCase deleteChecklistUseCase,
  GetCheckListUseCase getCheckListUseCase,
  MarkNodeAsDoneUseCase markNodeAsDoneUseCase,
  RemoveNodeUseCase removeNodeUseCase,
  UnmarkNodeAsDoneUseCase unmarkNodeAsDoneUseCase,
  UpdateNodeUseCase updateNodeUseCase
  ) :ControllerBase 
{

  private readonly AddNodeUseCase _addNodeUseCase = addNodeUseCase;
  private readonly CreateChecklistUseCase _createChecklistUseCase = createChecklistUseCase;
  private readonly DeleteChecklistUseCase _deleteChecklistUseCase = deleteChecklistUseCase;
  private readonly GetCheckListUseCase _getCheckListUseCase = getCheckListUseCase;
  private readonly MarkNodeAsDoneUseCase _markNodeAsDoneUseCase= markNodeAsDoneUseCase;
  private readonly RemoveNodeUseCase _removeNodeUseCase = removeNodeUseCase;
  private readonly UnmarkNodeAsDoneUseCase _unmarkNodeAsDoneUseCase = unmarkNodeAsDoneUseCase;
  private readonly UpdateNodeUseCase _updateNodeUseCase = updateNodeUseCase;

  [HttpPost]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> CreateChecklist(CreateChecklistDTO createChecklistDTO)
  {
    await _createChecklistUseCase.Execute(createChecklistDTO.AssignmentId);
    return Created();
  }

  [HttpPost("addNode")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> AddNodeChecklist(AddNodeDTO addNodeDTO)
  {
    await _addNodeUseCase.Execute(addNodeDTO);
    return Ok();
  }

  [HttpGet("{assignmentId:guid}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> GetChecklist(Guid assignmentId)
  {
    var checkList = await _getCheckListUseCase.Execute(assignmentId);
    return Ok(checkList);
  }

  [HttpDelete("{checklistId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> DeleteChecklist(long checklistId)
  {
    await _deleteChecklistUseCase.Execute(checklistId);
    return Ok();
  }

  [HttpDelete("removeNode/{nodeId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> RemoveNodeFromChecklist(long nodeId)
  {
    await _removeNodeUseCase.Execute(nodeId);
    return Ok();
  }

  [HttpPatch("markAsDoneNode/{nodeId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> MarkAsDoneNode(long nodeId)
  {
    await _markNodeAsDoneUseCase.Execute(nodeId);
    return Ok();
  }

  [HttpPatch("unmarkAsDoneNode/{nodeId:long}")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UnmarkAsDoneNode(long nodeId)
  {
    await _unmarkNodeAsDoneUseCase.Execute(nodeId);
    return Ok();
  }

  [HttpPut]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public async Task<IActionResult> UpdateNode(UpdateNodeDTO nodeDTO)
  {
    await _updateNodeUseCase.Execute(nodeDTO);
    return Ok();
  }

}