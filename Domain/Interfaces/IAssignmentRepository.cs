using MercurialBackendDotnet.Domain.Entities;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface IAssignmentRepository
{
  Task CreateAssignmentAsync(Assignment assignment);

  Task DeleteAssignmentAsync(string assignmentId);

  Task UpdateAssingmetAsync(Assignment assignment);

  Task<Assignment> GetAssignmentByIdAync(string assignmentId);

  Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(string userId);
}