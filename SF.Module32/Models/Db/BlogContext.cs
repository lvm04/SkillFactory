using Microsoft.EntityFrameworkCore;

namespace MvcStartApp.Models;

/// <summary>
/// Класс контекста, предоставляющий доступ к сущностям базы данных
/// </summary>
public sealed class BlogContext : DbContext
{
    public DbSet<User> Users { get; set; }  
    public DbSet<UserPost> UserPosts { get; set; }
    public DbSet<Request> Requests { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<UserPost>().ToTable("UserPost");
    }
}