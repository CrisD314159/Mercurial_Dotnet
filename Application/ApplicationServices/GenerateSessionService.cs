using System.Runtime.CompilerServices;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.ApplicationServices;

public class GenerateSessionService(
  UserManager<User> userManager,
  ISessionsRepository sessionsRepository,
  IJWTService jwtService)
{
  private readonly UserManager<User> _userManager = userManager;
  private readonly ISessionsRepository _sessionRepository = sessionsRepository;
  private readonly IJWTService _jwtService = jwtService;
  
  public async Task<string> CreateSession(string userId, string email)
  {
    var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("User not found");
    var userSessions = await _sessionRepository.GetUserOldSessionsAsync(user.Id);
    if (userSessions.Count > 0)
    {
      await _sessionRepository.RemoveSeveralSessionsAsync(userSessions);
    }
    var session = new Session()
    {
      User = user,
      ExpiresAt = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
      SignedAt = DateOnly.FromDateTime(DateTime.UtcNow),
    };

    await _sessionRepository.CreateSession(session);

    return _jwtService.GenerateToken(userId, email, session.Id, true);
  }
}