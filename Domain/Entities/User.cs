using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Domain.Model;


public class User : IdentityUser
{
  [MaxLength(100)]
  public required string Name { get; set; }

  public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

  public required UserState State { get; set; }

  [Url]
  public required string ProfilePicture { get; set; }

  public required DateOnly LastUpdatedAt { get; set; }

  public required string VerificationCode { get; set; }

  public required bool IsThirdPartyUser { get; set; }

  public ICollection<Topic> UserTopics { get; set; } = [];

  public ICollection<Subject> UserSubjects { get; set; } = [];

  public ICollection<Assignment> UserAssignments { get; set; } = []; 
  
  public bool VerifyValidUser()
  {
    if (State == UserState.DELETED) throw new EntityNotFoundException("User Not found");
    if (State == UserState.NOT_VERIFIED) throw new UnauthorizedException("You're not verified yet");
    return true;
  }
 
}