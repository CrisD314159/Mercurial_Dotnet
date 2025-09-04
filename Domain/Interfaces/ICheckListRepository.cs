using MercurialBackendDotnet.Domain.Entities;

namespace MercurialBackendDotnet.Domain.Interfaces;


public interface ICheckListRepository
{
  Task CreateCheckListAsync(CheckList subject);

  Task DeleteChecklistAsync(string checklistId);

  Task AddNodeToChecklist(CheckListItem node, string checklistId);
  Task RemoveNodeFromChecklist(CheckListItem node, string checklistId);

  Task<CheckList> GetChecklistByIdAync(string checklistId);

}