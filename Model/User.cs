using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Model;


public class User
{
  public required string Id {get; set;}

  [MaxLength(100)]
  public required string Name {get; set;}

  public required DateOnly CreatedAt {get; set;}

  public required string AccountId {set; get;}

  public required Account UserAccount {get; set;}

  public required UserState State {get; set;}

  [Url]
  public required string ProfilePicture {get; set;}

  public required DateOnly LastUpdatedAt {get; set;}

  public ICollection<Topic> UserTopics {get; set;} = []; 

  public ICollection<Subject> UserSubjects {get; set;} = []; 

  public ICollection<Task> UserTasks {get; set;} = []; 
 
}