using Microsoft.EntityFrameworkCore;
using SF.Module25.Model;

namespace SF.Module25.Repositories;


public class UserRepository
{
    public IEnumerable<User> FindAll()
    {
        using (var db = new AppDbContext())
        {
            return db.Users.Include(d => d.Books).AsNoTracking().ToList();
        }
    }

    public User? FindById(int id)
    {
        using (var db = new AppDbContext())
        {
            return db.Users.Find(id);
        }
    }

    public void Delete(int id)
    {
        try
        {
            using (var db = new AppDbContext())
            {
                User delUser = new User { Id = id };
                db.Users.Attach(delUser);
                db.Users.Remove(delUser);
                db.SaveChanges();
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("Ошибка удаления записи");
        }
       
    }

    public void Delete(User user)
    {
        if (user != null)
            Delete(user.Id);
        else
            Console.WriteLine("Ошибка удаления записи");
    }

    public void Create(User user)
    {
        using (var db = new AppDbContext())
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
    }

    public void UpdateYear(int id, string newName)
    {
        using (var db = new AppDbContext())
        {
            User? user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = newName;
                db.SaveChanges();
            }
        }
    }

    // Получать количество книг на руках у пользователя
    public int QtyBooksHasUser(int userId)
    {
        using (var db = new AppDbContext())
        {
            // var books = db.Users
            //     .Join(db.Books, u => u.Id, b => b.UserId, (u, b) => new { uId = u.Id, bName = b.Name })
            //     .Where(d => d.uId == userId);
            // Console.WriteLine(books.ToQueryString());

            var user = db.Users.Include(b => b.Books).Where(d => d.Id == userId).FirstOrDefault();

            return user != null ? user.Books.Count() : 0;
        }
    }

}
