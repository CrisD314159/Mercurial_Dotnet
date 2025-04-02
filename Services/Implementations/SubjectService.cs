using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class SubjectService (MercurialDBContext dBContext) : ISubjectService
{

  private readonly MercurialDBContext _dbContext = dBContext;

  public Task CreateSubjectDTO(Guid userId, CreateSubjectDTO createSubjectDTO)
  {
    throw new NotImplementedException();
  }

  public Task DeleteSubject(long subjectId)
  {
    throw new NotImplementedException();
  }

  public Task<GetUserSubjectsDTO> GetUserSubjects(Guid userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public Task UpdateSubject(UpdateSubjectDTO updateSubjectDTO)
  {
    throw new NotImplementedException();
  }
}