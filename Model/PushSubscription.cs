namespace MercurialBackendDotnet.Model;

public class PushSubscription
{
  public required Guid Id {set; get;} = Guid.NewGuid();
  public required Guid UserId {set; get;}
  public required DateOnly CreatedAr {set; get;}
  public required string UserAgent {set; get;}

}