using System.Security.Claims;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.UseCases.Account;


public class Logout(IJWTService jwtService, ISessionsRepository sessionsRepository)
{

  private readonly IJWTService _jwtService = jwtService;
  private readonly ISessionsRepository _sessionsRepository = sessionsRepository;


  public async Task Execute(string refreshToken)
  {
    var claims = _jwtService.ExtractRefreshToken(refreshToken, out var securityToken);

    var sessionId = claims.FindFirst(ClaimTypes.Authentication)?.Value.ToString()
    ?? throw new EntityNotFoundException("Session not founc");
    var session = await _sessionsRepository.GetSessionByIdAsync(sessionId);

    await _sessionsRepository.RemoveSingleSessionAsync(session);
  }
  
}