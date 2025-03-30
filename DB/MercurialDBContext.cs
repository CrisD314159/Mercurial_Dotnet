using MercurialBackendDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.DB;


public class MercurialDBContext (DbContextOptions<MercurialDBContext> options) : DbContext(options)
{

  public required DbSet<User> Users {set; get;}

  public required DbSet<Topic> Topics {set; get;}

  public required DbSet<Assignment> Tasks {set; get;}

  public required DbSet<Subject> Subjects {set; get;}

  public required DbSet<Session> Sessions {set; get;}

  public required DbSet<PushSubscription> PushSubscriptions {set; get;}

  public required DbSet<Note> Notes {set; get;}
  
  public required DbSet<CheckListItem> CheckListItems {set; get;}

  public required DbSet<CheckList> CheckLists {set; get;}
  
  public required DbSet<Account> Accounts {set; get;}


}