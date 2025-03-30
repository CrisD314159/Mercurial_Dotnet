namespace MercurialBackendDotnet.Model;

public class CheckList
{
  public required long Id {set; get;}
  
  public ICollection<CheckListItem> CheckListItems {get; set;} = [];

  public required Guid AssignmentId {set; get;}

  public required Assignment Assignment {set; get;}

  public required DateOnly CreatedAt {set; get;}

  public required DateOnly LastUpdatedAt {set; get;}

}