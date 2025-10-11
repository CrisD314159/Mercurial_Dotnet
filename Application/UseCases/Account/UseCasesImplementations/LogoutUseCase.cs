using System.Security.Claims;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Application.UseCases.Account.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.UseCases.Account.UseCasesImplementations;


public class LogoutUseCase(IJwtService jwtService, ISessionsRepository sessionsRepository): ILogoutUseCase
{

  private readonly IJwtService _jwtService = jwtService;
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