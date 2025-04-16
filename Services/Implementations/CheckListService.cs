using FluentValidation;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class CheckListService (MercurialDBContext dbContext,
IValidator<AddNodeDTO> validator, IValidator<UpdateNodeDTO> validatorUpdate
) : ICheckListService
{
  private readonly MercurialDBContext _dbContext =dbContext;
  private readonly IValidator<AddNodeDTO> _validatorCreate = validator;
  private readonly IValidator<UpdateNodeDTO> _validatorUpdate = validatorUpdate;

/// <summary>
/// Adds a new node to the selected list
/// </summary>
/// <param name="addNodeDTO"></param>
/// <returns></returns>
/// <exception cref="EntityNotFoundException"></exception>
/// <exception cref="ExceededLimitException"></exception>
  public async Task AddNode(AddNodeDTO addNodeDTO)
  {
    _validatorCreate.ValidateAndThrow(addNodeDTO);

    var list = await _dbContext.CheckLists.Include(l => l.CheckListItems)
    .FirstOrDefaultAsync(l => l.Id == addNodeDTO.ListId) ?? throw new EntityNotFoundException("List not found");

    if(list.CheckListItems.Count > 14) throw new ExceededLimitException("Each list can only have up to 15 elements");

    CheckListItem node = new (){
      Content = addNodeDTO.Content,
      IsCompleted = false,
      CheckList = list
    };
    list.CheckListItems.Add(node);
    await _dbContext.CheckListItems.AddAsync(node);
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Creates and adds a checklist to the user account
  /// </summary>
  /// <param name="taskId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task CreateCheckList(Guid taskId)
  {
    var assignment = await _dbContext.Assignments.FindAsync(taskId)
    ?? throw new EntityNotFoundException("Assignment not found");

    if(assignment.HasChecklist) throw new VerificationException("This assignment already has a checklist");

    CheckList checkList = new (){
      Assignment = assignment,
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
    };
    assignment.CheckList = checkList;
    assignment.HasChecklist = true;
    await _dbContext.CheckLists.AddAsync(checkList);
    await _dbContext.SaveChangesAsync();
    
  }

  /// <summary>
  /// Deletes permanently a checklist an all its nodes
  /// </summary>
  /// <param name="listId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task DeleteCheckList(long listId)
  {
    var checkList = await _dbContext.CheckLists.Include(c => c.CheckListItems)
    .Where(c => c.Id == listId).FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Checklist not found");

    var assignment = await _dbContext.Assignments.Include(a => a.CheckList).Where(a => a.CheckList != null && a.CheckList.Id == listId)
    .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Assignment not found");

    _dbContext.CheckLists.Remove(checkList);
    assignment.CheckList = null;
    assignment.HasChecklist = false;
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Gets a list with all its nodes
  /// </summary>
  /// <param name="listId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task<GetChecklistDTO> GetChecklist(Guid assignmentId)
  {
    var checklist = await _dbContext.CheckLists.Include(c => c.CheckListItems)
    .Where(c => c.AssignmentId == assignmentId).Select(s => new {
      s.Id,
      Nodes = s.CheckListItems.Select(i => new NodeDTO(i.Id, i.Content, i.IsCompleted)).ToList()
    }).FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Checklist not found");

    return new GetChecklistDTO(checklist.Id, checklist.Nodes);
    
  }

  /// <summary>
  /// Marks a list node as done (true)
  /// </summary>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task MarkAsDoneNode(long nodeId)
  {
    var node = await _dbContext.CheckListItems.FindAsync(nodeId)
    ?? throw new EntityNotFoundException("Node not found");

    node.IsCompleted = true;
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Removes a deletes a node from a checklist permanently
  /// </summary>
  /// <param name="listId"></param>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task RemoveNode(long nodeId)
  {

    var node = await _dbContext.CheckListItems.FindAsync(nodeId)
    ?? throw new EntityNotFoundException("Node not found");

    var checkList = await _dbContext.CheckLists.Include(c => c.CheckListItems)
    .FirstOrDefaultAsync(c => c.Id == node.CheckListId) 
    ?? throw new EntityNotFoundException("Checklist Not found");

    checkList.CheckListItems.Remove(node);
    _dbContext.CheckListItems.Remove(node);
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Unmarks a list ndde as assigned (false)
  /// </summary>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task UnmarkAsDoneNode(long nodeId)
  {
    var node = await _dbContext.CheckListItems.FindAsync(nodeId)
    ?? throw new EntityNotFoundException("Node not found");

    node.IsCompleted = false;
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateNode(UpdateNodeDTO updateNodeDTO)
  {
    _validatorUpdate.ValidateAndThrow(updateNodeDTO);

    var node = await _dbContext.CheckListItems.FindAsync(updateNodeDTO.NodeId)
    ?? throw new EntityNotFoundException("Node not found");

    node.Content = updateNodeDTO.Content;
    await _dbContext.SaveChangesAsync();
  }
}