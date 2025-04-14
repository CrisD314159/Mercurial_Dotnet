namespace MercurialBackendDotnet.Model;

public class Session
{
  public string Id {set; get;} = Guid.NewGuid().ToString();

  public string UserId {set; get;} = null!;

  public required User User {set; get;}

  public string? Fingerprint {set; get;}

  public required DateOnly ExpiresAt {set; get;}

  public required DateOnly SignedAt {set; get;}

}