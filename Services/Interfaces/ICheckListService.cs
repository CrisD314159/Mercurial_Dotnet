using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ICheckListService
{
  Task CreateCheckList (Guid taskId);

  Task AddNode(AddNodeDTO addNodeDTO);

  Task<GetChecklistDTO> GetChecklist(Guid assignmentId);

  Task RemoveNode(long nodeId);

  Task MarkAsDoneNode(long nodeId);

  Task UnmarkAsDoneNode(long nodeId);

  Task DeleteCheckList(long listId);

  Task UpdateNode(UpdateNodeDTO updateNodeDTO);


}