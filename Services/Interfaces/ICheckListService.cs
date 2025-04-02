using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ICheckListService
{
  Task CreateCheckList (long taskId);

  Task AddNode(AddNodeDTO addNodeDTO);

  Task<GetChecklistDTO> GetChecklist(long listId);

  Task RemoveNode(long listId, long nodeId);

  Task MarkAsDoneNode(long nodeId);

  Task UnmarkAsDoneNode(long nodeId);

  Task DeleteCheckList(long listId);


}