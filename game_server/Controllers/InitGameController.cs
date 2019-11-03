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
    private GameSessionModelDBContext _context;
    private games.ICardGame _cardGameService;

    public InitGameController(
      ILogger<InitGameController> logger,
      GameSessionModelDBContext context,
      games.ICardGame cardGame)
    {
        _logger = logger;
        _context = context;
        _cardGameService = cardGame;
    }

    [EnableCors]
    [HttpGet("{userEmail}/{gameName}")]
    public InitGamePayload Get(string userEmail, string gameName){
      // TODO - User reg
      var user_player = _context.Users.Where(x => x.EmailAddress == userEmail).Single();
      var ai_player = _context.Users.Where(x => x.EmailAddress == "ai@escoba.com").Single();

      // TODO - Specific gamename resolution
      var escoba_info = _context.Games.Where(x => x.GameName == "escoba").Single();
      var initial_game_state = _cardGameService.InitGame();

      var gs = new GameSession {
        GameSessionId = System.Guid.NewGuid(),
        SelectedGameInfo = escoba_info,
        SelectedGameInfoId = escoba_info.GameInfoId, 
        GameSessionState = "running",
        UserPlayers = new List<UserGameSession>(),
        GameStates = new List<games.GameState>(),
      };

      gs.UserPlayers.Add(new UserGameSession {
        GameSession = gs,
        GameSessionId = gs.GameSessionId,
        User = user_player,
        UserId = user_player.UserId,
      });

      gs.GameStates.Add(initial_game_state);
      
      var payload = new Models.InitGamePayload {
        SessionId = System.Guid.NewGuid(),
        GameState = initial_game_state,
      };
      return payload;
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
