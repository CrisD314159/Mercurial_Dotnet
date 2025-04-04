using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Model;


public class User
{
  public Guid Id {get; set;} = Guid.NewGuid();

  [MaxLength(100)]
  public required string Name {get; set;}

  public DateOnly CreatedAt {get; set;} = DateOnly.FromDateTime(DateTime.UtcNow);

  public Account Account {get; set;} = null!;

  public required UserState State {get; set;}

  [Url]
  public required string ProfilePicture {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}

  public ICollection<Topic> UserTopics {get; set;} = []; 

  public ICollection<Subject> UserSubjects {get; set;} = []; 

  public ICollection<Assignment> UserAssignments {get; set;} = []; 
 
}