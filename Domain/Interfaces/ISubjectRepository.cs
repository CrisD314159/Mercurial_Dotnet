using MercurialBackendDotnet.Domain.Entities;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface ISubjectRepository
{
  Task CreateSubjectAsync(Subject subject);

  Task DeleteSubjectId(string subjectId);

  Task UpdateAssingmetAsync(Subject subject);

  Task<Subject> GetSubjectByIdAync(string subjectId);

  Task<IEnumerable<Subject>> GetUserSubjectsAsync(string userId);
}