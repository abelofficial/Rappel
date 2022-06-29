using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    { }

    public DbSet<User> Users { get; set; }

    public DbSet<Todo> Todos { get; set; }

    public DbSet<SubTask> SubTasks { get; set; }

    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<SubTask>()
           .Property(u => u.Status)
           .HasConversion<string>()
           .HasMaxLength(50);
    }
}