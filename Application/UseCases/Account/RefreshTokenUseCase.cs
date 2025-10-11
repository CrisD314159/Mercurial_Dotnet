using System.Security.Claims;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.Account;


public class RefreshTokenUseCase (IjwtService jwtService, ISessionsRepository sessionsRepository)
{

  private readonly IjwtService _jwtService = jwtService;
  private readonly ISessionsRepository _sessionsRepository = sessionsRepository;

  public async Task<RefreshTokenResponseDTO> Execute(string refreshToken)
  {
    var claims = _jwtService.ExtractRefreshToken(refreshToken, out var securityToken);

    var sessionId = claims.FindFirst(ClaimTypes.Authentication)?.Value.ToString()
    ?? throw new EntityNotFoundException("Claim not found");

    var session = await _sessionsRepository.GetSessionByIdAsync(sessionId);

    var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString()
    ?? throw new EntityNotFoundException("Claim not found");

    var email = claims.FindFirst(ClaimTypes.Email)?.Value.ToString()
    ?? throw new EntityNotFoundException("Claim not found");

    if (session.ExpiresAt < DateOnly.FromDateTime(DateTime.UtcNow))
      throw new UnauthorizedException("Expired session");

    var token = _jwtService.GenerateToken(userId, email, "", false);

    if (DateOnly.FromDateTime(DateTime.UtcNow) >= session.ExpiresAt.AddDays(-2))
    {
      var refresh = _jwtService.GenerateToken(userId, email, sessionId, true);

      return new RefreshTokenResponseDTO(token, refresh);

    }
    return new RefreshTokenResponseDTO(token, null);

  }
}