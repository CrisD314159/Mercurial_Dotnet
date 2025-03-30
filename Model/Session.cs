namespace MercurialBackendDotnet.Model;

public class Session
{
  public required long Id {set; get;}

  public required Guid UserId {set; get;}

  public required User User {set; get;}

  public string? Fingerprint {set; get;}

  public required DateOnly ExpresAt {set; get;}

}