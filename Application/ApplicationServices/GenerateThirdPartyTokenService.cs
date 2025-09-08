using System.Security;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.ApplicationServices;


public class GenerateThirdPartyTokenService(IJWTService jwtService, GenerateSessionService generateSessionService)
{

  private readonly IJWTService _jwtService = jwtService;
  private readonly GenerateSessionService _generateSessionService = generateSessionService;

  public async Task<LoginResponseDTO> Execute(string id, string email)
  {
    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email))
      throw new VerificationException("id or email not provided");

    var token = _jwtService.GenerateToken(id, email, "", false);
    var refreshToken = await _generateSessionService.CreateSession(id, email);

    return new LoginResponseDTO(token, refreshToken);
  }
}