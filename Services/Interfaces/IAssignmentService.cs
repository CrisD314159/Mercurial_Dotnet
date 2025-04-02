using System.Runtime.InteropServices;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAssignmentService
{
  Task CreateAssignment(Guid userId, CreateTaskDTO createTaskDTO);

  Task UpdateAssignment(UpdateAssignmentDTO updateTaskDTO);

  Task DeleteAssignment(Guid taskId);

  Task<GetUserAssignmentsDTO> GetUserTodoTasks(Guid userId, int offset, int limit);
  
  Task<GetUserAssignmentsDTO> GetUserDoneTasks(Guid userId, int offset, int limit);
}