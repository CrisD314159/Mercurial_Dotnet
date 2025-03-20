namespace MercurialBackendDotnet.Model;

public class Session
{
  public required string Id {set; get;}

  public required string UserId {set; get;}

  public required User User {set; get;}

  public string? Fingerprint {set; get;}

  public required DateOnly ExpresAt {set; get;}

}