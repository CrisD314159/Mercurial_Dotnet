namespace MercurialBackendDotnet.Model;

public class CheckList
{
  public long Id {set; get;}
  
  public ICollection<CheckListItem> CheckListItems {get; set;} = [];

  public Guid AssignmentId {set; get;}

  public required Assignment Assignment {set; get;} 

  public DateOnly CreatedAt {set; get;} =  DateOnly.FromDateTime(DateTime.UtcNow);

  public required DateOnly LastUpdatedAt {set; get;}

}