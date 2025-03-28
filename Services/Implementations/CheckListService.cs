using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class CheckListService (MercurialDBContext dbContext) : ICheckListService
{
  private readonly MercurialDBContext _dbContext =dbContext;
  public void AddNode(AddNodeDTO addNodeDTO)
  {
    throw new NotImplementedException();
  }

  public void CreateCheckList(string taskId)
  {
    throw new NotImplementedException();
  }

  public void DeleteCheckList(string listId)
  {
    throw new NotImplementedException();
  }

  public GetChecklistDTO GetChecklis(string listId)
  {
    throw new NotImplementedException();
  }

  public void MarkAsDoneNode(string nodeId)
  {
    throw new NotImplementedException();
  }

  public void RemoveNode(string nodeId)
  {
    throw new NotImplementedException();
  }

  public void UnmarkAsDoneNode(string nodeId)
  {
    throw new NotImplementedException();
  }
}