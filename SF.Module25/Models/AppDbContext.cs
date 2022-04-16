using Microsoft.EntityFrameworkCore;

namespace SF.Module25.Model;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    public AppDbContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = books.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        User user1 = new User { Id = 1, Name = "Анатолий", Email = "tolya@mail.ru" }; 
        User user2 = new User { Id = 2, Name = "Николай", Email = "kolya@mail.ru" }; 
        User user3 = new User { Id = 3, Name = "Мария", Email = "masha@mail.ru" };
        User user4 = new User { Id = 4, Name = "Ольга", Email = "olya@mail.ru" };
        modelBuilder.Entity<User>().HasData(user1, user2, user3, user4);

        Book book1 = new Book { Id = 1, Name = "Книга-1", Year = 1968, Author = "Автор-1", Genre = "Драма", UserId = 1 };
        Book book2 = new Book { Id = 2, Name = "Книга-2", Year = 1971, Author = "Автор-1", Genre = "Детектив", UserId = 2 };
        Book book3 = new Book { Id = 3, Name = "Книга-3", Year = 2004, Author = "Автор-2", Genre = "Фантастика", UserId = 1 };
        Book book4 = new Book { Id = 4, Name = "Книга-4", Year = 2011, Author = "Автор-3", Genre = "Детектив", UserId = 2 };
        Book book5 = new Book { Id = 5, Name = "Книга-5", Year = 1995, Author = "Автор-4", Genre = "Экономика", UserId = 2 };
        Book book6 = new Book { Id = 6, Name = "Книга-6", Year = 1983, Author = "Автор-5", Genre = "Женский роман", UserId = 3 };
        Book book7 = new Book { Id = 7, Name = "Книга-7", Year = 2018, Author = "Автор-6", Genre = "Кулинария", UserId = 3 };
        Book book8 = new Book { Id = 8, Name = "Книга-8", Year = 2009, Author = "Автор-3", Genre = "Боевик", UserId = null };
        Book book9 = new Book { Id = 9, Name = "Книга-09", Year = 2005, Author = "Автор-7", Genre = "Детектив", UserId = null };
        modelBuilder.Entity<Book>().HasData(book1, book2, book3, book4, book5, book6, book7, book8, book9);

        // Внешний ключ
        modelBuilder.Entity<Book>().HasOne(b => b.User).WithMany(u => u.Books).OnDelete(DeleteBehavior.SetNull);

    }
}