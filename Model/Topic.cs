using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercurialBackendDotnet.Model;


public class Topic
{
  public long Id {get; set;}

  [MaxLength(70)]
  public required string Title {get; set;}

  [MaxLength(9)]
  public required string Color {get; set;}

  public DateOnly CreatedAt {get; set;} = DateOnly.FromDateTime(DateTime.UtcNow);

  public  string UserId {get; set;} = null!;

  public required User User {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}

}