using System.ComponentModel.DataAnnotations;

namespace MercurialBackendDotnet.Model;

public class Note
{
  public long Id {get; set;}
  
  [MaxLength(500)]
  public string? Content {set; get;}

  public DateOnly CreatedAt {set; get;} =  DateOnly.FromDateTime(DateTime.UtcNow);

  public Guid AssignmentId {set; get;}

  public Assignment Assignment {set; get;} = null!;

}