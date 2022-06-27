using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models;

namespace MvcStartApp.Controllers;

public class LogsController : Controller
{
    private readonly IRequestRepository _repo;

    public LogsController(IRequestRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var requests = await _repo.GetRequests();
        return View(requests);
    }
}