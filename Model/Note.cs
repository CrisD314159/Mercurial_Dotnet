namespace MercurialBackendDotnet.Model;

public class Note
{
  public long Id {get; set;}
  
  public string? Content {set; get;}

  public required DateOnly CreatedAt {set; get;}

  public required Guid AssignmentId {set; get;}

  public required Assignment Assignment {set; get;}

}