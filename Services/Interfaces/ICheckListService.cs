using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ICheckListService
{
  void CreateCheckList (string taskId);

  void AddNode(AddNodeDTO addNodeDTO);

  GetChecklistDTO GetChecklis(string listId);

  void RemoveNode(string nodeId);

  void MarkAsDoneNode(string nodeId);

  void UnmarkAsDoneNode(string nodeId);

  void DeleteCheckList(string listId);


}