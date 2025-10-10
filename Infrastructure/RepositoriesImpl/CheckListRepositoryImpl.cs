using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

public class CheckListRepositoryImpl (MercurialDBContext mercurialDBContext):ICheckListRepository
{

    private readonly MercurialDBContext _dbContext = mercurialDBContext;
    public async Task CreateCheckListAsync(CheckList checklist, Assignment assignment)
    {
        await _dbContext.CheckLists.AddAsync(checklist);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteChecklistAsync(CheckList checklist, Assignment assignment)
    {
        assignment.CheckList = null;
        _dbContext.CheckLists.Remove(checklist);
        await _dbContext.SaveChangesAsync();
    }

    public async Task MarkNodeAsDoneAndSaveAsync(CheckListItem checkListItem)
    {
        checkListItem.IsCompleted = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UnMarkNodeAsDoneAndSaveAsync(CheckListItem checkListItem)
    {
        checkListItem.IsCompleted = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddNodeToChecklistAsync(CheckListItem node, CheckList checkList)
    {
        checkList.CheckListItems.Add(node);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveNodeFromChecklistAsync(CheckListItem node, CheckList checkList)
    {
        checkList.CheckListItems.Remove(node);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CheckListItem> GetCheckListItemAsync(long checklistItemId)
    {
        var item = await _dbContext.CheckListItems.FindAsync(checklistItemId)
            ?? throw new EntityNotFoundException("Checklist item not found");
        return item;
    }

    public async Task UpdateNodeAsync(CheckListItem checkListItem)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CheckList> GetChecklistWithNodesByIdAync(long checklistId)
    {
        var checkList = await _dbContext.CheckLists
        .Include(c => c.CheckListItems)
        .Where(c => c.Id == checklistId)
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Checklist not found");

        return checkList;
    }

    public async Task<GetChecklistDTO> GetChecklistDTOAsync(Guid assignmentId)
    {
        var checkList = await _dbContext.CheckLists
        .Include(c => c.CheckListItems)
        .Where(c => c.AssignmentId == assignmentId)
        .Select(c => new GetChecklistDTO(
            c.Id,
            c.CheckListItems
                .Select(item => new NodeDTO(
                    item.Id,
                    item.Content,
                    item.IsCompleted
                )).ToList()
        ))
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Checklist not found");

        return checkList;
    }
}