using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace MercurialBackendDotnet.Application.ExternalServices;

public interface IJwtService
{
  string GenerateToken(string userId, string email, string sessionId, bool generateRefresh);
  ClaimsPrincipal ExtractRefreshToken(string refreshToken, out SecurityToken securityToken);
}