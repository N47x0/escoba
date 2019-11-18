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
  public class PlayGameController : Controller
  {
    private readonly ILogger<PlayGameController> _logger;
    private GameSessionModelDBContext _context;
    private games.ICardGame _cardGameService;

    public PlayGameController(
      ILogger<PlayGameController> logger,
      GameSessionModelDBContext context,
      games.ICardGame cardGame)
    {
        _logger = logger;
        _context = context;
        _cardGameService = cardGame;
    }

    [EnableCors]
    [HttpPost("GetValidPlays/{sessionid}")]
    async public Task<ValidPlaysPayload> GetValidPlays([FromBody]ValidPlaysIncomingPayload incomingPayload){
      Console.WriteLine(incomingPayload);
      // TODO - Specific gamename resolution
      var escoba_info = _context.Games.Where(x => x.GameName == "escoba").SingleOrDefault();
      // List<List<Card>> valid_plays = _cardGameService.ValidPlays();
      List<List<games.Card>> valid_plays = new List<List<games.Card>>(); 

      Guid sessionid = incomingPayload.SessionId;

      GameSession gameSession;
      // Look up GameSession
      gameSession = _context.GameSessions.Find(sessionid);
      var current_game_state = gameSession.GameStates.OrderBy(x => x.TurnCount).LastOrDefault();

      await _context.SaveChangesAsync();
      var payload = new Models.ValidPlaysPayload {
        SessionId = gameSession.GameSessionId,
        GameState = current_game_state
      };
      return payload;
    }

    [EnableCors]
    [HttpPost("PlayNextTurn/{sessionid}")]
    async public Task<ValidPlaysPayload> PlayNextTurn([FromBody]PlayTurnIncomingPayload incomingPayload){
      Console.WriteLine(incomingPayload);
      // TODO - Specific gamename resolution
      var escoba_info = _context.Games.Where(x => x.GameName == "escoba").SingleOrDefault();
      // List<List<Card>> valid_plays = _cardGameService.ValidPlays();

      Guid sessionid = incomingPayload.SessionId;

      GameSession gameSession;
      // Look up GameSession
      gameSession = _context.GameSessions.Find(sessionid);
      var current_game_state = gameSession.GameStates.OrderBy(x => x.TurnCount).LastOrDefault();

      await _context.SaveChangesAsync();
      var payload = new Models.ValidPlaysPayload {
        SessionId = gameSession.GameSessionId,
        GameState = _cardGameService.PlayTurn(incomingPayload.CardsPlayed, current_game_state.CurrentPlayer, current_game_state)
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
