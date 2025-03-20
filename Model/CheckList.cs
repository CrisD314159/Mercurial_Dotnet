namespace MercurialBackendDotnet.Model;

public class CheckList
{
  public required String Id {set; get;}
  
  public ICollection<CheckListItem> CheckListItems {get;} = [];

  public required String TaskId {set; get;}

  public required Task Task {set; get;}

  public required DateOnly CreatedAt {set; get;}

  public required DateOnly LastUpdatedAt {set; get;}

}