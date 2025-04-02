using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class AssignmentService (MercurialDBContext dBContext) : IAssignmentService
{
  private readonly MercurialDBContext _dbContext = dBContext;

  public Task CreateAssignment(Guid userId, CreateTaskDTO createTaskDTO)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAssignment(Guid taskId)
  {
    throw new NotImplementedException();
  }

  public Task<GetUserAssignmentsDTO> GetUserDoneTasks(Guid userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public Task<GetUserAssignmentsDTO> GetUserTodoTasks(Guid userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public Task UpdateAssignment(UpdateAssignmentDTO updateTaskDTO)
  {
    throw new NotImplementedException();
  }
}