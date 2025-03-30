using System.ComponentModel.DataAnnotations;

namespace MercurialBackendDotnet.Model;

public class Subject
{
  public required long Id {get; set;}

  [MaxLength(100)]
  public required string Name {get; set;}

  public required DateOnly CreatedAt {get; set;}

  public required Guid UserId {get; set;}

  public required User User {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}
}