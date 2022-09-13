using Identity.EntityFramework.API.Models;
using Microsoft.EntityFrameworkCore;
using uBeac.Repositories.EntityFramework;

namespace Identity.EntityFramework.API.DBContext;

public class ApplicationDbContext : EFDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(b => b.Courses)
            .WithMany(x => x.Students);

        modelBuilder.Entity<Student>()
            .HasMany(b => b.Docs)
            .WithOne()
            .HasForeignKey(x => x.StudentId);
    }
    public DbSet<Student> Students { get; set; }    
    public DbSet<Course> Courses { get; set; }
    public DbSet<Doc> Docs { get; set; }

}

