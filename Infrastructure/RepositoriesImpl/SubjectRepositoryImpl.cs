using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;


public class SubjectRepositoryImpl:ISubjectRepository
{
    public Task CreateSubjectAsync(Subject subject)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSubjectAsync(Subject subject)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSubjectAsync(Subject subject)
    {
        throw new NotImplementedException();
    }

    public Task<Subject> GetSubjectByIdAync(string subjectId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserHasExceededSubjectsLimit(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Subject> GetSubjectBySubjectNameAndUserId(string subjectName, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Subject> GetSubjectBySubjectIdAndUserId(long subjectId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<SubjectDTO>> GetUserSubjectsAsync(string userId, int offset, int limit)
    {
        throw new NotImplementedException();
    }
}