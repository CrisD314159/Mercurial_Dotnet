using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Model;



public class Assignment
{
  public required Guid Id {set; get;} =  Guid.NewGuid();

  [MaxLength(100)]
  public required string Title {set; get;}

  public required long NoteId {set; get;}

  public required Note Note {set; get;}

  public required long SubjectId {set; get;}
  
  public required Subject Subject {set; get;}

  public required long TopicId {set; get;}

  public required Topic Topic {set; get;}

  public required Guid UserId {set; get;}

  public required User User {set; get;}

  public long? CheckListId {set; get;}

  public CheckList? CheckList {set; get;}

  public required DateTime DueDate {set; get;}

  public required DateOnly CreaatedAt {set; get;}

  public required AssignmentState TaskState {set ; get;}

  public required DateOnly LastUpdatedAt {set ; get;}
}