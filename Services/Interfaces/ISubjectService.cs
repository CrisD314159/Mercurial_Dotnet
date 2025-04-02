using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ISubjectService
{
  Task CreateSubjectDTO(Guid userId, CreateSubjectDTO createSubjectDTO);

  Task UpdateSubject(UpdateSubjectDTO updateSubjectDTO);

  Task DeleteSubject(long subjectId);

  Task<GetUserSubjectsDTO> GetUserSubjects(Guid userId, int offset, int limit);
}