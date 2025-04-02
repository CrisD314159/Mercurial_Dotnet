using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class CheckListService (MercurialDBContext dbContext) : ICheckListService
{
  private readonly MercurialDBContext _dbContext =dbContext;

/// <summary>
/// Adds a new node to the selected list
/// </summary>
/// <param name="addNodeDTO"></param>
/// <returns></returns>
/// <exception cref="EntityNotFoundException"></exception>
/// <exception cref="ExceededLimitException"></exception>
  public async Task AddNode(AddNodeDTO addNodeDTO)
  {
    var list = await _dbContext.CheckLists.Include(l => l.CheckListItems).Where(l => l.Id == addNodeDTO.ListId)
    .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("List not found");

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
  public async Task CreateCheckList(long taskId)
  {
    var assignment = await _dbContext.Assignments.FindAsync(taskId)
    ?? throw new EntityNotFoundException("Assignment not found");

    CheckList checkList = new (){
      Assignment = assignment,
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
    };
    assignment.CheckList = checkList;
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

    var assignment = await _dbContext.Assignments.Where(a => a.CheckListId == listId)
    .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Assignment not found");

    _dbContext.CheckListItems.RemoveRange(checkList.CheckListItems);
    _dbContext.CheckLists.Remove(checkList);
    assignment.CheckList = null;
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Gets a list with all its nodes
  /// </summary>
  /// <param name="listId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task<GetChecklistDTO> GetChecklist(long listId)
  {
    var checklist = await _dbContext.CheckLists.Include(c => c.CheckListItems)
    .Where(c => c.Id == listId).Select(s => new {
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
  public async Task RemoveNode(long listId, long nodeId)
  {
    var checkList = await _dbContext.CheckLists.Include(c => c.CheckListItems)
    .Where(c => c.Id == listId).FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Checklist not found");

    var node = await _dbContext.CheckListItems.FindAsync(nodeId)
    ?? throw new EntityNotFoundException("Node not found");

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
}