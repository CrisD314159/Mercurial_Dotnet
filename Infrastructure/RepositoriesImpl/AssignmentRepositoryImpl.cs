using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

public class AssignmentRepositoryImpl(MercurialDBContext dbContext): IAssignmentRepository
{
    private readonly MercurialDBContext _dbContext = dbContext;

    public async Task CreateAssignmentAsync(Assignment assignment, Note note)
    {
        await _dbContext.Notes.AddAsync(note);
        await _dbContext.Assignments.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAssignmentAsync(Assignment assignment)
    {
        _dbContext.Assignments.Remove(assignment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAssingmentAsync(Assignment assignment)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Assignment> GetAssignmentByIdAync(Guid assignmentId)
    {
       var assignment = await _dbContext.Assignments.FindAsync(assignmentId) ??
           throw new EntityNotFoundException("Assignment not found");
       return assignment;
    }

    public async Task<bool> UserHasExeededMaximumAssignments(string userId)
    {
        var userAssignments = await _dbContext.Assignments.Where(a => a.UserId == userId).CountAsync();

        return userAssignments >= 100;
    }

    public async Task<Assignment> GetAssignmentByAssignmentNameUserIdAndState(string name, string userId, AssignmentState assignmentState, long subjectId)
    {
       var assignment = await _dbContext.Assignments.Include(a => a.Note)
       .Where(a => a.Title == name && a.UserId == userId && a.TaskState == assignmentState && a.SubjectId == subjectId)
           .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Assignment not found");

       return assignment;

    }

    public async Task<Assignment> GetAssignmentByAssignmentIdAndUserId(Guid assignmentId, string userId)
    {
        var assignment = await _dbContext.Assignments.Include(a => a.Note).Where(a => a.Id == assignmentId && a.UserId == userId)
            .FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Assignment not found");

        return assignment;
    }

    public async Task<List<AssignmentDTO>> GetUserAssignmentsAsync(string userId, int offset, int limit, AssignmentState assignmentState)
    {
        var assignments = await _dbContext.Assignments.Include(a => a.Note)
            .Where(a => a.UserId == userId && a.TaskState == assignmentState)
            .Select(a => new AssignmentDTO(
                a.Id,
                a.Title,
                a.LastUpdatedAt,
                a.DueDate,
                a.TaskState,
                a.SubjectId,
                a.Subject.Name,
                a.Topic.Id,
                a.Topic.Title,
                a.Topic.Color,
                a.Note.Id,
                a.Note.Content ?? ""
            ))
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return assignments;
    }
}