using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercurialBackendDotnet.Model;

public class CheckListItem
{
  public required string Id {set; get;}

  [MaxLength(150)]
  public required string Content {set; get;}

  public required bool IsCompleted {set; get;}

  public required string CheckListId {set; get;}

  public required CheckList CheckList {set; get;}


}