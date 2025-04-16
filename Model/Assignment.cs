using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Model;



public class Assignment
{
  public Guid Id {set; get;} =  Guid.NewGuid();

  [MaxLength(100)]
  public required string Title {set; get;}

  public  Note Note {set; get;} = null!;

  public long SubjectId {set; get;}
  
  public required Subject Subject {set; get;}

  public long TopicId {set; get;}

  public required Topic Topic {set; get;}

  public string UserId {set; get;} = null!;

  public required User User {set; get;}

  public CheckList? CheckList {set; get;} = null!;

  public required bool HasChecklist {set; get;}

  public DateTime? DueDate {set; get;}

  public DateOnly CreatedAt {set; get;} = DateOnly.FromDateTime(DateTime.UtcNow);

  public required AssignmentState TaskState {set ; get;}

  public required DateOnly LastUpdatedAt {set ; get;}
}