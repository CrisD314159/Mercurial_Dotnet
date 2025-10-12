using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;


public class GenerateThirdPartyTokenService(IJwtService jwtService, IGenerateSessionService generateSessionService): IGenerateThirdPartyTokenService
{

  private readonly IJwtService _jwtService = jwtService;
  private readonly IGenerateSessionService _generateSessionService = generateSessionService;

  public async Task<LoginResponseDTO> Execute(string id, string email)
  {
    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email))
      throw new EntityValidationException("id or email not provided");

    var token = _jwtService.GenerateToken(id, email, "", false);
    var refreshToken = await _generateSessionService.CreateSession(id, email);

    return new LoginResponseDTO(token, refreshToken);
  }
}