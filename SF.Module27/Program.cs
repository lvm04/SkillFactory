var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.MapGet("/", async context =>
{
    var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "index.html");
    var html = await File.ReadAllTextAsync(viewPath);
    await context.Response.WriteAsync(html);
});

app.MapGet("/Static/css/index.css", async context =>
{
    // по аналогии со страницей Index настроим на нашем сервере путь до страницы со стилями, чтобы браузер знал, откуда их загружать
    var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "css", "index.css");
    var css = await File.ReadAllTextAsync(cssPath);
    await context.Response.WriteAsync(css);
});

app.Run();
