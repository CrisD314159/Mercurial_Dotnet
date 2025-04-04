using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercurialBackendDotnet.Model;

public class Account
{
  public Guid Id {set; get;} = Guid.NewGuid();

  [EmailAddress]
  public required string Email {set; get;}

  public required string Password {set; get;}

  public Guid UserId {set; get;}

  public User User {set; get;} = null!;

  public ICollection<Session> AccountSessions {get; set;} = [];

  [MaxLength(4)]
  public required int VerificationCode {get; set;}

}