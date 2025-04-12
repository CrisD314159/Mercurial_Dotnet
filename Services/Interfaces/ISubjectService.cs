using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ISubjectService
{
  Task CreateSubjectDTO(string userId, CreateSubjectDTO createSubjectDTO);

  Task UpdateSubject(UpdateSubjectDTO updateSubjectDTO);

  Task DeleteSubject(long subjectId);

  Task<GetUserSubjectsDTO> GetUserSubjects(string userId, int offset, int limit);
}