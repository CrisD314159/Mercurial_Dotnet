using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Model;


public class User
{
  public required Guid Id {get; set;} = Guid.NewGuid();

  [MaxLength(100)]
  public required string Name {get; set;}

  public required DateOnly CreatedAt {get; set;}

  public required Guid AccountId {set; get;}

  public required Account Account {get; set;}

  public required UserState State {get; set;}

  [Url]
  public required string ProfilePicture {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}

  public ICollection<Topic> UserTopics {get; set;} = []; 

  public ICollection<Subject> UserSubjects {get; set;} = []; 

  public ICollection<Assignment> UserAssignments {get; set;} = []; 
 
}