using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class SubjectService (MercurialDBContext dBContext) : ISubjectService
{

  private readonly MercurialDBContext _dbContext = dBContext;
  public void CreateSubjectDTO(CreateSubjectDTO createSubjectDTO)
  {
    throw new NotImplementedException();
  }

  public void DeleteSubject(string subjectId)
  {
    throw new NotImplementedException();
  }

  public GetUserSubjectsDTO GetUserSubjects(string userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public void UpdateSubject(UpdateSubjectDTO updateSubjectDTO)
  {
    throw new NotImplementedException();
  }
}