using System.ComponentModel.DataAnnotations;

namespace MercurialBackendDotnet.Model;

public class Account
{
  public required string Id {set; get;}

  [EmailAddress]
  public required string Email {set; get;}

  public required string Password {set; get;}

  public required string UserId {set; get;}

  public required User User {set; get;}

}