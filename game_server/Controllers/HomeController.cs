using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using game_server.Models;

namespace game_server.Controllers
{
  public class HomeController : Controller
  {
    private game_server.GameSessionModelDbContext _context;
    private game_server.IGameSessionModelDbContextFactory _contextFactory;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, game_server.IGameSessionModelDbContextFactory contextFactory)
    {
      _logger = logger;
        this._contextFactory = contextFactory;
        _context = _contextFactory.CreateDbContext(new string[] {"Test8"});
    }

    public IActionResult Index()
    {
      var first_user = _context.Users.First();
      ViewData["FromDB"] = first_user.FirstName + first_user.LastName;
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
