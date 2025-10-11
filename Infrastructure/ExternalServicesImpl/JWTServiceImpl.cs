

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ExternalServices;
using Microsoft.IdentityModel.Tokens;

namespace MercurialBackendDotnet.Infrastructure.ExternalServicesImpl;

public class JWTServiceImpl(IConfiguration configuration):IjwtService
{

  private readonly IConfiguration _configuration = configuration;


    public string GenerateToken(string userId, string email, string sessionId, bool generateRefresh)
    {
      var claims = new List<Claim>
      {
        new(ClaimTypes.NameIdentifier, userId),
        new(ClaimTypes.Email, email)
      };

      if(!string.IsNullOrEmpty(sessionId))
      {
        claims.Add(new(ClaimTypes.Authentication, sessionId));
      }

      var jwtKey = generateRefresh ? _configuration["Jwt:RefreshKey"]: _configuration["Jwt:Key"];

      if (string.IsNullOrEmpty(jwtKey))
      {
          throw new EntityValidationException( "JWT key cannot be null or empty.");
      }
      var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
      var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(

        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims:claims,
        expires: generateRefresh ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ExtractRefreshToken(string refreshToken, out SecurityToken securityToken)
    {
      var handler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshKey"] ?? throw new EntityValidationException("Key not found"));

      var validationParameters = new TokenValidationParameters()
      {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ClockSkew = TimeSpan.Zero

      };

      try
      {
        var result = handler.ValidateToken(refreshToken, validationParameters , out securityToken);

        return result;

      }catch(SecurityTokenMalformedException)
      {
        throw new EntityValidationException("Invalid token type");
      }
      catch(SecurityTokenExpiredException)
      {
        throw new UnauthorizedException("Expired session");
      }
      catch(SecurityTokenException)
      {
        throw new UnauthorizedException("Token not allowed");
      }
    }
}

