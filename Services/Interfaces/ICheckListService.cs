using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ICheckListService
{
  Task CreateCheckList (long taskId);

  Task AddNode(AddNodeDTO addNodeDTO);

  Task<GetChecklistDTO> GetChecklis(long listId);

  Task RemoveNode(long nodeId);

  Task MarkAsDoneNode(long nodeId);

  Task UnmarkAsDoneNode(long nodeId);

  Task DeleteCheckList(long listId);


}