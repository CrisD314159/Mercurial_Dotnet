using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Model;


public class User : IdentityUser
{
  [MaxLength(100)]
  public required string Name {get; set;}

  public DateOnly CreatedAt {get; set;} = DateOnly.FromDateTime(DateTime.UtcNow);

  public required UserState State {get; set;}

  [Url]
  public required string ProfilePicture {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}

  public required string VerificationCode {get; set;}
  
  public required bool IsThirdPartyUser { get; set; }

  public ICollection<Topic> UserTopics { get; set; } = []; 

  public ICollection<Subject> UserSubjects {get; set;} = []; 

  public ICollection<Assignment> UserAssignments {get; set;} = []; 
 
}