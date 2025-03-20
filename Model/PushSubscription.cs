namespace MercurialBackendDotnet.Model;

public class PushSubscription
{
  public required string Id {set; get;}
  public required string UserId {set; get;}
  public required DateOnly CreatedAr {set; get;}
  public required string UserAgent {set; get;}

}