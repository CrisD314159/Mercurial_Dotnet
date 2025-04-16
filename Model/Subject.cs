using System.ComponentModel.DataAnnotations;

namespace MercurialBackendDotnet.Model;

public class Subject
{
  public long Id {get; set;}

  [MaxLength(70)]
  public required string Name {get; set;}

  public DateOnly CreatedAt {get; set;} = DateOnly.FromDateTime(DateTime.UtcNow);

  public string UserId {get; set;} = null!;

  public required User User {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}
}