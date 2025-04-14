using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MercurialBackendDotnet.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace MercurialBackendDotnet.Utils.JWT;

public static class JWTService{



  public static string GenerateToken(string userId, string email, string sessionId, bool generateRefresh, IConfiguration configuration)
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

    var jwtKey = generateRefresh ? configuration["Jwt:RefreshKey"]: configuration["Jwt:Key"];

    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new VerificationException( "JWT key cannot be null or empty.");
    }
    var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(

      issuer: configuration["Jwt:Issuer"],
      audience: configuration["Jwt:Audience"],
      claims:claims,
      expires: generateRefresh ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(1),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);

  }


  public static ClaimsPrincipal ExtractRefreshToken(string refreshToken, IConfiguration configuration, out SecurityToken securityToken)
  {
    var handler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(configuration["Jwt:RefreshKey"] ?? throw new VerificationException("Key not found"));

    var validationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero

    };

    var result = handler.ValidateToken(refreshToken, validationParameters , out securityToken) 
    ?? throw new UnauthorizedException("Token not allowed");

    return result;
    
  }
  
}