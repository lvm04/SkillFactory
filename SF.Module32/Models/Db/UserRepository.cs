using Microsoft.EntityFrameworkCore;

namespace MvcStartApp.Models;

public class UserRepository : IUserRepository
{
    // ссылка на контекст
    private readonly BlogContext _context;

    // Метод-конструктор для инициализации
    public UserRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        user.JoinDate = DateTime.Now;
        user.Id = Guid.NewGuid();

        // Добавление пользователя
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            _context.Users.Add(user);

        // Сохранение изенений
        await _context.SaveChangesAsync();
    }

    public async Task<User[]> GetUsers()
    {
        // Получим всех активных пользователей
        return await _context.Users.AsNoTracking().ToArrayAsync();
    }
}