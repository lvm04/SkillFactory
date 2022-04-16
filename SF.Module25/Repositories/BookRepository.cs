using Microsoft.EntityFrameworkCore;
using SF.Module25.Model;

namespace SF.Module25.Repositories;


public class BookRepository
{
    public IEnumerable<Book> FindAll(string sortBy = "")
    {
        using (var db = new AppDbContext())
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    return db.Books.OrderBy(b => b.Name).AsNoTracking().ToList();
                case "year desc":
                    return db.Books.OrderByDescending(b => b.Year).AsNoTracking().ToList();
                default:
                    return db.Books.AsNoTracking().ToList();
            }
        }
    }

    public Book? FindById(int id)
    {
        using (var db = new AppDbContext())
        {
            return db.Books.Find(id);
        }
    }

    public void Delete(int id)
    {
        try
        {
            using (var db = new AppDbContext())
            {
                Book delBook = new Book { Id = id };
                db.Books.Attach(delBook);
                db.Books.Remove(delBook);
                db.SaveChanges();
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("Ошибка удаления записи");
        }
        
    }

    public void Delete(Book book)
    {
        if (book != null)
            Delete(book.Id);
        else
            Console.WriteLine("Ошибка удаления записи");
    }

    public void Create(Book book)
    {
        using (var db = new AppDbContext())
        {
            db.Books.Add(book);
            db.SaveChanges();
        }
    }

    public void UpdateYear(int id, int newYear)
    {
        using (var db = new AppDbContext())
        {
            Book? book = db.Books.FirstOrDefault(u => u.Id == id);
            if (book != null)
            {
                book.Year = newYear;
                db.SaveChanges();
            }
        }
    }


    // Получать список книг определенного жанра и вышедших между определенными годами
    public IEnumerable<Book> GetBooksByGenreAndYears(string genre, int year1, int year2)
    {
        using (var db = new AppDbContext())
        {
            return db.Books.Where(b => b.Genre == genre && b.Year >= year1 && b.Year <= year2).ToList();
        }
    }

    // Получать количество книг определенного автора в библиотеке
    public int GetBooksCountByAuthor(string author)
    {
        using (var db = new AppDbContext())
        {
            return db.Books.Where(b => b.Author == author).Count();
        }
    }

    // Получать количество книг определенного жанра в библиотеке
    public int GetBooksCountByGenre(string genre)
    {
        using (var db = new AppDbContext())
        {
            return db.Books.Where(b => b.Genre == genre).Count();
        }
    }

    // Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке
    public bool IsTheBookAvailableByAuthor(string author, string name)
    {
        using (var db = new AppDbContext())
        {
            return db.Books.Any(b => b.Author == author && b.Name == name);
        }
    }

    // Получать булевый флаг о том, есть ли определенная книга на руках у пользователя
    public bool IsTheBookIssued(int bookId)
    {
        using (var db = new AppDbContext())
        {
            return db.Books.Any(b => b.Id == bookId && b.UserId != null);
        }
    }

    // Получение последней вышедшей книги
    public Book? GetLastBook()
    {
        using (var db = new AppDbContext())
        {
            return db.Books.OrderBy(b => b.Year).LastOrDefault();
        }
    }
}
