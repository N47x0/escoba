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
    private game_server.GameSessionModelDbContext _context;
    private game_server.IGameSessionModelDbContextFactory _contextFactory;
    private games.ICardGame _cardGameService;

    public PlayGameController(
      ILogger<PlayGameController> logger,
      game_server.IGameSessionModelDbContextFactory contextFactory,
      games.ICardGame cardGame)
    {
        _logger = logger;
        this._contextFactory = contextFactory;
        _context = _contextFactory.CreateDbContext(new string[] {"Test8"});
        _cardGameService = cardGame;
    }

    [EnableCors]
    [HttpPost("PlayNextTurn")]
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

      var payload = new Models.ValidPlaysPayload {
        SessionId = gameSession.GameSessionId,
        GameState = _cardGameService.PlayTurn(incomingPayload.CardsPlayed, current_game_state.CurrentPlayer, current_game_state)
      };

      gameSession.GameStates.Add(payload.GameState);
      await _context.SaveChangesAsync();
      return payload;
    }
  }
}
