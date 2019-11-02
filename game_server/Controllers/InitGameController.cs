using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using game_server.Models;

namespace game_server.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class InitGameController : Controller
  {
    private readonly ILogger<InitGameController> _logger;

    public InitGameController(ILogger<InitGameController> logger)
    {
        _logger = logger;
    }

    [EnableCors]
    [HttpGet]
    public string Get(){
      return "Hello!";
    }

//     [EnableCors]
//     [HttpGet]
//     public InitGamePayload Get(){

//       return new InitGamePayload {
//         gameState = new games.GameState {
//           CurrentPlayer = new games.Player {
            
//           }
//         }
//       }
//     }

//     [EnableCors]
//     [HttpPost]
//     public string Post(){

//     }

  }
}
