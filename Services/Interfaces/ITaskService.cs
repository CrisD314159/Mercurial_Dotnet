using System.Runtime.InteropServices;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ITaskService
{
  void CreateTask(CreateTaskDTO createTaskDTO);

  void UpdateTask(UpdateTaskDTO updateTaskDTO);

  void DeleteTask(string taskId);

  GetUserTasksDTO GetUserTodoTasks(string userId, int offset, int limit);
  GetUserTasksDTO GetUserDoneTasks(string userId, int offset, int limit);
}