using Microsoft.EntityFrameworkCore;

namespace MvcStartApp.Models;

public class RequestRepository : IRequestRepository
{
    // ссылка на контекст
    private readonly BlogContext _context;

    // Метод-конструктор для инициализации
    public RequestRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task AddRequest(Request request)
    {
        request.Id = Guid.NewGuid();
        request.Date = DateTime.Now;

        // Добавление запроса
        var entry = _context.Entry(request);
        if (entry.State == EntityState.Detached)
            _context.Requests.Add(request);

        // Сохранение изенений
        await _context.SaveChangesAsync();
    }

    public async Task<Request[]> GetRequests()
    {
        // Получим всех активных пользователей
        return await _context.Requests.AsNoTracking().ToArrayAsync();
    }
}