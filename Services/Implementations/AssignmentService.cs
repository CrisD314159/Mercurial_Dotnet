using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class AssignmentService (MercurialDBContext dBContext) : IAssignmentService
{
  private readonly MercurialDBContext _dbContext = dBContext;
  
  public void CreateTask(CreateTaskDTO createTaskDTO)
  {
    throw new NotImplementedException();
  }

  public void DeleteTask(string taskId)
  {
    throw new NotImplementedException();
  }

  public GetUserAssignmentsDTO GetUserDoneTasks(string userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public GetUserAssignmentsDTO GetUserTodoTasks(string userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public void UpdateTask(UpdateAssignmentDTO updateTaskDTO)
  {
    throw new NotImplementedException();
  }
}