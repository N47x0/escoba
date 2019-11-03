using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet("{userEmail}/{gameName}/{sessionid?}")]
    async public Task<InitGamePayload> Get(string userEmail, string gameName, Guid sessionid){
      // TODO - User reg
      var user_player = _context.Users
        .Include(e => e.Stats)
        .Include(e => e.GameSessions)        
        .Where(x => x.EmailAddress == userEmail)
        .SingleOrDefault();
      var ai_player = _context.Users.Where(x => x.EmailAddress == "ai@escoba.com").SingleOrDefault();

      // TODO - Specific gamename resolution
      var escoba_info = _context.Games.Where(x => x.GameName == "escoba").SingleOrDefault();
      var initial_game_state = _cardGameService.InitGame();

      GameSession gameSession;
      UserGameSession ugs;
      // Make New GameSession
      if (sessionid == System.Guid.Empty) {
        gameSession = new GameSession {
          GameSessionId = System.Guid.NewGuid(),
          SelectedGameInfo = escoba_info,
          SelectedGameInfoId = escoba_info.GameInfoId, 
          GameSessionState = "running",
          UserPlayers = new List<UserGameSession>(),
          GameStates = new List<games.GameState>(),
        };
        _context.GameSessions.Add(gameSession);
        ugs = new UserGameSession {
          GameSession = gameSession,
          GameSessionId = gameSession.GameSessionId,
          User = user_player,
          UserId = user_player.UserId,
        };
        // TODO - Is all this needed or some auto?
        gameSession.UserPlayers.Add(ugs);
        user_player.GameSessions.Add(ugs);
      } 
      // Lookup game session
      else {
        gameSession = _context.GameSessions.Find(sessionid);
        // Find current/logged-in user (url param)
        ugs = gameSession.UserPlayers.Where(x => x.User.EmailAddress == userEmail).SingleOrDefault();
      }

      gameSession.GameStates.Add(initial_game_state);
      
      await _context.SaveChangesAsync();
      var payload = new Models.InitGamePayload {
        SessionId = gameSession.GameSessionId,
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
