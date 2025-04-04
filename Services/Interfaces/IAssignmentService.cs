using System.Runtime.InteropServices;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAssignmentService
{
  Task CreateAssignment(Guid userId, CreateAssignmentDTO createAssignmentDTO);

  Task UpdateAssignment(UpdateAssignmentDTO updateTaskDTO);

  Task DeleteAssignment(Guid taskId);

  Task<GetUserAssignmentsDTO> GetUserTodoTasks(Guid userId, int offset, int limit);
  
  Task<GetUserAssignmentsDTO> GetUserDoneTasks(Guid userId, int offset, int limit);

  Task MarkAssignmentAsDone(Guid assignmentId);

  Task MarkAssigmentInprogress(Guid assignmentId);

  Task MarkAssigmentTodo(Guid assignmentId);
}