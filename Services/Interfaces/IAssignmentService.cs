using System.Runtime.InteropServices;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAssignmentService
{
  Task CreateAssignment(string userId, CreateAssignmentDTO createAssignmentDTO);

  Task UpdateAssignment(UpdateAssignmentDTO updateTaskDTO);

  Task DeleteAssignment(Guid taskId);

  Task<GetUserAssignmentsDTO> GetUserTodoTasks(string userId, int offset, int limit);
  
  Task<GetUserAssignmentsDTO> GetUserDoneTasks(string userId, int offset, int limit);

  Task MarkAssignmentAsDone(Guid assignmentId);

  Task MarkAssigmentInprogress(Guid assignmentId);

  Task MarkAssigmentTodo(Guid assignmentId);
}