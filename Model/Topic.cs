using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercurialBackendDotnet.Model;


public class Topic
{
  public long Id {get; set;}

  [MaxLength(100)]
  public required string Title {get; set;}

  [MaxLength(9)]
  public required string Color {get; set;}

  public DateOnly CreatedAt {get; set;} = DateOnly.FromDateTime(DateTime.Now);

  public  Guid UserId {get; set;}

  public required User User {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}

}