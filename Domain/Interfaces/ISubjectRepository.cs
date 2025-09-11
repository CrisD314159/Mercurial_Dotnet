using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface ISubjectRepository
{
  Task CreateSubjectAsync(Subject subject);

  Task DeleteSubjectAsync(Subject subject);

  Task UpdateSubjectAsync(Subject subject);

  Task<Subject> GetSubjectByIdAync(string subjectId);
  Task<bool> UserHasExceededSubjectsLimit(string userId);
  Task<Subject> GetSubjectBySubjectNameAndUserId(string subjectName, string userId);
  Task<Subject> GetSubjectBySubjectIdAndUserId(long subjectId, string userId);
  Task<List<SubjectDTO>> GetUserSubjectsAsync(string userId, int offset, int limit);
}