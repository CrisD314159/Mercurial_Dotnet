using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class CheckListService (MercurialDBContext dbContext) : ICheckListService
{
  private readonly MercurialDBContext _dbContext =dbContext;

  public Task AddNode(AddNodeDTO addNodeDTO)
  {
    throw new NotImplementedException();
  }

  public Task CreateCheckList(long taskId)
  {
    throw new NotImplementedException();
  }

  public Task DeleteCheckList(long listId)
  {
    throw new NotImplementedException();
  }

  public Task<GetChecklistDTO> GetChecklis(long listId)
  {
    throw new NotImplementedException();
  }

  public Task MarkAsDoneNode(long nodeId)
  {
    throw new NotImplementedException();
  }

  public Task RemoveNode(long nodeId)
  {
    throw new NotImplementedException();
  }

  public Task UnmarkAsDoneNode(long nodeId)
  {
    throw new NotImplementedException();
  }
}