using MercurialBackendDotnet.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.DB;


public class MercurialDBContext: IdentityDbContext<User>
{
     public MercurialDBContext(DbContextOptions<MercurialDBContext> options)
            : base(options)
        {
        }


  /// <summary>
  /// IdentityDbContext allow us to create a identity database, this includes 
  /// a few entities to manage user registration 
  /// 
  /// IdentityRole<Guid> its used to map a Guia to the user ID
  /// 
  /// It's not necesary to instance a user DBset, IdentityDbContext does it for default
  /// </summary>
  public required DbSet<Topic> Topics {set; get;}

  public required DbSet<Assignment> Assignments {set; get;}

  public required DbSet<Subject> Subjects {set; get;}

  public required DbSet<Session> Sessions {set; get;}

  public required DbSet<PushSubscription> PushSubscriptions {set; get;}

  public required DbSet<Note> Notes {set; get;}
  
  public required DbSet<CheckListItem> CheckListItems {set; get;}

  public required DbSet<CheckList> CheckLists {set; get;}


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    // This allows EF Core to use cascade deletion for 1 to many relations

    base.OnModelCreating(modelBuilder);
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