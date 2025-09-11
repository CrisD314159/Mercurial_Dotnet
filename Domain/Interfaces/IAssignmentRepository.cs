using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface IAssignmentRepository
{
  Task CreateAssignmentAsync(Assignment assignment, Note note);

  Task DeleteAssignmentAsync(Assignment assignment);

  Task UpdateAssingmentAsync(Assignment assignment);

  Task<Assignment> GetAssignmentByIdAync(string assignmentId);

  Task<bool> UserHasExeededMaximumAssignments(string userId);
  Task<Assignment> GetAssignmentByAssignmentNameUserIdAndState(string name, string userId, AssignmentState assignmentState);
  Task<Assignment> GetAssignmentByAssignmentIdAndUserId(Guid assignmentId, string userId);

  Task<List<AssignmentDTO>> GetUserAssignmentsAsync(string userId, int offset, int limit, AssignmentState assignmentState);
}