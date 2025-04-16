using System.Runtime.InteropServices;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IAssignmentService
{
  Task CreateAssignment(string userId, CreateAssignmentDTO createAssignmentDTO);

  Task UpdateAssignment(string userId, UpdateAssignmentDTO updateTaskDTO);

  Task DeleteAssignment(string userId, Guid assignmentId);

  Task<GetUserAssignmentsDTO> GetUserTodoTasks(string userId, int offset, int limit);
  
  Task<GetUserAssignmentsDTO> GetUserDoneTasks(string userId, int offset, int limit);

  Task MarkAssignmentAsDone(string userId, Guid assignmentId);

  Task MarkAssigmentInprogress(string userId, Guid assignmentId);

  Task MarkAssigmentTodo(string userId, Guid assignmentId);
}