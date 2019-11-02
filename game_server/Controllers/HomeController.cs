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
    private GameSessionModelDBContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, GameSessionModelDBContext context)
    {
      _logger = logger;
      _context = context;
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
