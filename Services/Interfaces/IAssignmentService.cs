using System.Runtime.InteropServices;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAssignmentService
{
  void CreateTask(CreateTaskDTO createTaskDTO);

  void UpdateTask(UpdateAssignmentDTO updateTaskDTO);

  void DeleteTask(string taskId);

  GetUserAssignmentsDTO GetUserTodoTasks(string userId, int offset, int limit);
  GetUserAssignmentsDTO GetUserDoneTasks(string userId, int offset, int limit);
}