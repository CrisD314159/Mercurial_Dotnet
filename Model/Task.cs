using System.ComponentModel.DataAnnotations;
using MercurialBackendDotnet.Model.Enums;

namespace MercurialBackendDotnet.Model;



public class Task
{
  public required string Id {set; get;}

  [MaxLength(100)]
  public required string Title {set; get;}

  public required string NoteId {set; get;}

  public required Note Note {set; get;}

  public required string SubjectId {set; get;}
  
  public required Subject Subject {set; get;}

  public required string TopicId {set; get;}

  public required Topic Topic {set; get;}

  public required string UserId {set; get;}

  public required User User {set; get;}

  public string? CheckListId {set; get;}

  public CheckList? CheckList {set; get;}

  public required DateTime DueDate {set; get;}

  public required DateOnly CreaatedAt {set; get;}

  public required TaskState TaskState {set ; get;}

  public required TaskState LastUpdatedAt {set ; get;}
}