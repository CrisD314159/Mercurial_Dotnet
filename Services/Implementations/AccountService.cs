using System.Security.Claims;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Model.Enums;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Utils;
using MercurialBackendDotnet.Utils.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class AccountService(MercurialDBContext dbContext, SignInManager<User> signInManager,
UserManager<User> userManager, IConfiguration configuration) : IAccountService
{
  private readonly MercurialDBContext _dbContext = dbContext;
  private readonly SignInManager<User> _signInManager = signInManager;
  private readonly UserManager<User> _userManager = userManager;
  private readonly IConfiguration _configuration = configuration;

  public async Task<RefreshTokenResponseDTO> RefreshToken(string refreshToken)
  {
    var claims = JWTService.ExtractRefreshToken(refreshToken, _configuration, out var securityToken);

    var sessionId = claims.FindFirst(ClaimTypes.Authentication)?.Value.ToString()
    ?? throw new EntityNotFoundException("Claim not found");

    var session = await _dbContext.Sessions.FindAsync(sessionId) ?? throw new EntityNotFoundException("Session not found");

    var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString()
    ?? throw new EntityNotFoundException("Claim not found");

    var email = claims.FindFirst(ClaimTypes.Email)?.Value.ToString()
    ?? throw new EntityNotFoundException("Claim not found");

    if(session.ExpiresAt < DateOnly.FromDateTime(DateTime.UtcNow)) throw new UnauthorizedException("Expired session");

    var token = JWTService.GenerateToken(userId, email, "", false, _configuration);

    if (DateOnly.FromDateTime(DateTime.UtcNow) >= session.ExpiresAt.AddDays(-2))
    {
      var refresh = JWTService.GenerateToken(userId, email, sessionId, true, _configuration);

      return new RefreshTokenResponseDTO(token, refresh);

    }
    return new RefreshTokenResponseDTO(token, null);
  }

  public async Task<LoginResponseDTO> Login(LoginDTO loginDTO)
  {
    var user = await _userManager.FindByEmailAsync(loginDTO.Email) 
    ?? throw new EntityNotFoundException("User not found");
   
    VerifyValidUser(user.State); // Verifies the user state

    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

    if (!result.Succeeded)
    {
      throw new VerificationException("Invalid email or password");
    }

    if (result.Succeeded && user.Email != null)
    {
      var token = JWTService.GenerateToken(user.Id, user.Email, "", false, _configuration);
      var refreshToken = await GenerateSession(user.Id, user.Email);

      return new LoginResponseDTO(token, refreshToken);
    }

    throw new InternalServerException("Cannot Login");
  }

  public bool VerifyValidUser(UserState userState)
  {
    if(userState == UserState.DELETED) throw new EntityNotFoundException("User Not found");
    if(userState == UserState.NOT_VERIFIED) throw new UnauthorizedException("You're not verified yet");
    return true;
  }

  public async Task<string> GenerateSession(string userId, string email)
  {
    var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("User not found");
    var userSessions = await _dbContext.Sessions.Where(s => s.UserId == userId && s.SignedAt <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5))).ToListAsync();
    if (userSessions.Count > 0){
      _dbContext.Sessions.RemoveRange(userSessions);
    }
    var session = new Session()
    {
      User = user,
      ExpiresAt = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
      SignedAt = DateOnly.FromDateTime(DateTime.UtcNow),
    };

    await _dbContext.Sessions.AddAsync(session);
    await _dbContext.SaveChangesAsync();

    return JWTService.GenerateToken(userId, email, session.Id, true, _configuration);
  }

  public async Task Logout(string refreshToken)
  {
    var claims = JWTService.ExtractRefreshToken(refreshToken, _configuration, out var securityToken);

    var sessionId = claims.FindFirst(ClaimTypes.Authentication)?.Value.ToString();
    var session = await _dbContext.Sessions.FindAsync(sessionId)
     ?? throw new EntityNotFoundException("Session not found");

    _dbContext.Sessions.Remove(session);
    await _dbContext.SaveChangesAsync();
  }

  public async Task SendAccountCreatedVerificationCode(string name, string email, string code)
  {
    await EmailUtil.ReadFileToSendEmail(name, "Verify your email", email, "Thanks for sign up to Mercurial, use this code to verify your account", 
    code, "emailTemplate");
  }

  public async Task SendRecoverAccountVerificationCode(string name, string email, string code)
  {
    var encodedCode = Uri.EscapeDataString(code);
    var link = $"https://mercurial-app.vercel.app/changePassword?changeToken={encodedCode}";
    await EmailUtil.ReadFileToSendEmail(name, "Recover your account", email, "Use this code to recover your account :)", link, "recoveryEmailTemplate");
  }


}