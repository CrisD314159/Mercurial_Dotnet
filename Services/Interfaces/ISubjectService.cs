using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface ISubjectService
{
  void CreateSubjectDTO(CreateSubjectDTO createSubjectDTO);

  void UpdateSubject(UpdateSubjectDTO updateSubjectDTO);

  void DeleteSubject(string subjectId);

  GetUserSubjectsDTO GetUserSubjects(string userId, int offset, int limit);
}