using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

public class SessionsRepositoryImpl(MercurialDBContext mercurialDBContext):ISessionsRepository
{
    private readonly MercurialDBContext _dbContext = mercurialDBContext;

    public async Task<List<Session>> GetUserSessionsAsync(string userId)
    {
        var userSessions = await _dbContext.Sessions
        .Where(s => s.UserId == userId)
        .ToListAsync();

        return userSessions;
    }

    public async Task<List<Session>> GetUserOldSessionsAsync(string userId)
    {
        var userOldSessions = await _dbContext.Sessions
        .Where(s => s.UserId == userId && s.SignedAt <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)))
        .ToListAsync();

        return userOldSessions;
    }

    public async Task<Session> GetSessionByIdAsync(string sessionId)
    {
        var session = await _dbContext.Sessions.FindAsync(sessionId)
        ?? throw new EntityNotFoundException("Session not found");

        return session;
    }

    public async Task RemoveSingleSessionAsync(Session session)
    {
        _dbContext.Sessions.Remove(session);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveSeveralSessionsAsync(List<Session> sessions)
    {
        _dbContext.Sessions.RemoveRange(sessions);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateSession(Session session)
    {
        await _dbContext.Sessions.AddAsync(session);
        await _dbContext.SaveChangesAsync();
    }
}