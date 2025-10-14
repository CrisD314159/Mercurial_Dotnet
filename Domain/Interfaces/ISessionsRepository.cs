using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Model;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface ISessionsRepository
{
  Task<List<Session>> GetUserSessionsAsync(string userId);
  Task<List<Session>> GetUserOldSessionsAsync(string userId);
  Task<Session> GetSessionByIdAsync(string sessionId);
  Task RemoveSingleSessionAsync(Session session);

  Task RemoveSeveralSessionsAsync(List<Session> sessions);

  Task CreateSession(Session session);
}