using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercurialBackendDotnet.Model;

public class Account
{
  public required Guid Id {set; get;} = Guid.NewGuid();

  [EmailAddress]
  public required string Email {set; get;}

  public required string Password {set; get;}

  public required string UserId {set; get;}

  public required User User {set; get;}

  public required ICollection<Session> AccountSessions {get; set;} = [];

  [MaxLength(4)]
  public required int VerificationCode {get; set;}

}