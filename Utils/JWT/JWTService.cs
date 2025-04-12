using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MercurialBackendDotnet.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace MercurialBackendDotnet.Utils.JWT;

public static class JWTGeneration{



  public static string GenerateToken(Guid userId, string email, bool generateRefresh, IConfiguration configuration)
  {

    var claims = new []
    {
      new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
      new Claim(ClaimTypes.Email, email)
    };
    
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
  
}