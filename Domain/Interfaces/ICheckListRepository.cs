using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Domain.Interfaces;


public interface ICheckListRepository
{
  Task CreateCheckListAsync(CheckList checklist, Assignment assignment);

  Task DeleteChecklistAsync(CheckList checklist, Assignment assignment);

  Task MarkNodeAsDoneAndSaveAsync(CheckListItem checkListItem);
  Task UnMarkNodeAsDoneAndSaveAsync(CheckListItem checkListItem);
  Task AddNodeToChecklistAsync(CheckListItem node, CheckList checkList);
  Task RemoveNodeFromChecklistAsync(CheckListItem node, CheckList checkList);
  Task<CheckListItem> GetCheckListItemAsync(long checklistItemId);
  Task UpdateNodeAsync(CheckListItem checkListItem);

  Task<CheckList> GetChecklistWithNodesByIdAync(long checklistId);
  Task<GetChecklistDTO> GetChecklistDTOAsync(Guid assignmentId);

}