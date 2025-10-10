using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

public class SessionsRepositoryImpl:ISessionsRepository
{
    public Task<List<Session>> GetUserSessionsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Session>> GetUserOldSessionsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Session> GetSessionByIdAsync(string sessionId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveSingleSessionAsync(Session session)
    {
        throw new NotImplementedException();
    }

    public Task RemoveSeveralSessionsAsync(List<Session> sessions)
    {
        throw new NotImplementedException();
    }

    public Task CreateSession(Session session)
    {
        throw new NotImplementedException();
    }
}