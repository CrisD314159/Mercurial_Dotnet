namespace MercurialBackendDotnet.Model;

public class Note
{
  public string? Content {set; get;}

  public required DateOnly CreatedAt {set; get;}

  public required string TaskId {set; get;}

  public required Task Task {set; get;}

}