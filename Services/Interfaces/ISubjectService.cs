using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ISubjectService
{
  Task CreateSubjectDTO(string userId, CreateSubjectDTO createSubjectDTO);

  Task UpdateSubject(string userId, UpdateSubjectDTO updateSubjectDTO);

  Task DeleteSubject(string userId, long subjectId);

  Task<GetUserSubjectsDTO> GetUserSubjects(string userId, int offset, int limit);
}