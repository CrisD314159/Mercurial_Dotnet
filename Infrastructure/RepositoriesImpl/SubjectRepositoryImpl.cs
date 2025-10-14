using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;


public class SubjectRepositoryImpl(MercurialDBContext mercurialDBContext):ISubjectRepository
{

    private readonly MercurialDBContext _dbContext = mercurialDBContext;

    public async Task CreateSubjectAsync(Subject subject)
    {
        await _dbContext.Subjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(Subject subject)
    {
        _dbContext.Subjects.Remove(subject);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateSubjectAsync(Subject subject)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Subject> GetSubjectByIdAync(string subjectId)
    {
        var subject = await _dbContext.Subjects.FindAsync(subjectId)
        ?? throw new EntityNotFoundException("Subject not found");
        return subject;
    }

    public async Task<bool> UserHasExceededSubjectsLimit(string userId)
    {
        var userSubjects = await _dbContext.Subjects
        .Where(s => s.UserId == userId)
        .CountAsync();

        return userSubjects >= 15;
    }

    public async Task<Subject> GetSubjectBySubjectNameAndUserId(string subjectName, string userId)
    {
        var subject = await _dbContext.Subjects
        .Where(s => s.Name == subjectName && s.UserId == userId)
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Subject not found");

        return subject;

    }

    public async Task<Subject> GetSubjectBySubjectIdAndUserId(long subjectId, string userId)
    {
        var subject = await _dbContext.Subjects
        .Where(s => s.Id == subjectId && s.UserId == userId)
        .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Subject not found");

        return subject;
    }

    public async Task<List<SubjectDTO>> GetUserSubjectsAsync(string userId, int offset, int limit)
    {
        var userSubjects = await _dbContext.Subjects
        .Where(s => s.UserId == userId)
        .Select(s => new SubjectDTO(s.Id, s.Name, s.LastUpdatedAt))
        .Skip(offset)
        .Take(limit)
        .ToListAsync();

        return userSubjects;
    }
}