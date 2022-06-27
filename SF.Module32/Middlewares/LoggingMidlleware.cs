using MvcStartApp;
using MvcStartApp.Models;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context, IRequestRepository repo)
    {
        // Для логирования данных о запросе используем свойста объекта HttpContext
        context.Request.Headers.TryGetValue("Sec-Fetch-Dest", out var contentType);
        if (contentType.ToString() == "document")               // запросы картинок, скриптов и пр. не учитываем  
        {
            string url = $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.Path}";
            Console.WriteLine($"[{DateTime.Now}]: New request to {url}");
            string userAgent = context.Request.Headers.UserAgent.ToString();
            Console.WriteLine($"User-Agent: {userAgent}");

            // Сохраним запрос в БД
            Request r = new();
            r.Url = url;
            await repo.AddRequest(r);
        }

        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
}