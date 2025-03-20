using System.ComponentModel.DataAnnotations;

namespace MercurialBackendDotnet.Model;

public class Subject
{
  public required string Id {get; set;}

  [MaxLength(100)]
  public required string Name {get; set;}

  public required DateOnly CreatedAt {get; set;}

  public required string UserId {get; set;}

  public required User User {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}
}