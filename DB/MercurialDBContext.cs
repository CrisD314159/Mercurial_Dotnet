using MercurialBackendDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.DB;


public class MercurialDBContext (DbContextOptions<MercurialDBContext> options) : DbContext(options)
{

  public required DbSet<User> Users {set; get;}

  public required DbSet<Topic> Topics {set; get;}

  public required DbSet<Assignment> Assignments {set; get;}

  public required DbSet<Subject> Subjects {set; get;}

  public required DbSet<Session> Sessions {set; get;}

  public required DbSet<PushSubscription> PushSubscriptions {set; get;}

  public required DbSet<Note> Notes {set; get;}
  
  public required DbSet<CheckListItem> CheckListItems {set; get;}

  public required DbSet<CheckList> CheckLists {set; get;}
  
  public required DbSet<Account> Accounts {set; get;}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Assignment>()
    .HasOne(a => a.Note)
    .WithOne(n => n.Assignment)
    .HasForeignKey<Note>(n => n.AssignmentId)
    .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Assignment>()
        .HasOne(a => a.CheckList)
        .WithOne(c => c.Assignment)
        .HasForeignKey<CheckList>(c => c.AssignmentId)
        .OnDelete(DeleteBehavior.Cascade); 

    modelBuilder.Entity<CheckList>()
    .HasMany(c => c.CheckListItems)
    .WithOne(i => i.CheckList)
    .HasForeignKey(c => c.CheckListId)
    .OnDelete(DeleteBehavior.Cascade);


  }


}