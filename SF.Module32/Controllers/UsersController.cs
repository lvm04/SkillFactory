using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models;

namespace MvcStartApp.Controllers;

public class UsersController : Controller
{
    private readonly IUserRepository _repo;

    public UsersController(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _repo.GetUsers();
        return View(authors);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User newUser)
    {
        await _repo.AddUser(newUser);
        return View(newUser);
    }
}