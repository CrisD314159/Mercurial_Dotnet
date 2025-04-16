using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercurialBackendDotnet.Model;

public class CheckListItem
{
  public long Id {set; get;}

  [MaxLength(105)]
  public required string Content {set; get;}

  public required bool IsCompleted {set; get;}

  public long CheckListId {set; get;}

  public required CheckList CheckList {set; get;}
}