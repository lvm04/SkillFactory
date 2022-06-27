using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SF.Module35;
using SF.Module35.Data;
using SF.Module35.Data.Repository;
using SF.Module35.Extentions;
using SF.Module35.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddIdentity<User, IdentityRole>(opts => {
                                        opts.Password.RequiredLength = 3;
                                        opts.Password.RequireNonAlphanumeric = false;
                                        opts.Password.RequireLowercase = false;
                                        opts.Password.RequireUppercase = false;
                                        opts.Password.RequireDigit = false;
                                    })
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddUnitOfWork()
                    .AddCustomRepository<Message, MessageRepository>()
                    .AddCustomRepository<Friend, FriendsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
var cachePeriod = "0";
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
