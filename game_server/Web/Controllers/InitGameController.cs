using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using game_server.Context;
using game_server.Database.Models;
using game_server.Web.Converters;
using game_server.Web.DTO;
using game_server.Factory;

namespace game_server.Web.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class InitGameController : Controller
  {
    private readonly ILogger<InitGameController> _logger;
    private readonly IGameSessionModelDbContextFactory dbContextFactory;
    private games.ICardGame _cardGameService;

    public InitGameController(
      ILogger<InitGameController> logger,
      IGameSessionModelDbContextFactory dbContextFactory,
      games.ICardGame cardGame)
    {
        _logger = logger;
          this.dbContextFactory = dbContextFactory;
        _cardGameService = cardGame;
    }

    [EnableCors]
    [HttpGet("{userEmail}/{gameName}/{sessionid?}")]
    async public Task<InitGamePayload> Get(string userEmail, string gameName, Guid sessionid){
      // TODO - User reg
        using (var context = dbContextFactory.CreateDbContext())
        {
            var user_player = context.DbSet<User>()
                .Include(e => e.UserStatistics)
                .Include(e => e.GameSessions)        
                .Where(x => x.EmailAddress == userEmail)
                .SingleOrDefault();

            var ai_player = context.DbSet<User>()
                .Where(x => x.EmailAddress == "ai@escoba.com")
                .SingleOrDefault();

            // TODO - Specific gamename resolution
            var escoba_info = context.DbSet<GameInfo>()
                .Where(x => x.GameName == "escoba")
                .SingleOrDefault();

            var initial_game_state = _cardGameService
                .InitGame();

            GameSession gameSession;
            UserGameSession ugs;
            // Make New GameSession
            if (sessionid == System.Guid.Empty) {
                gameSession = new GameSession {
                    GameSessionId = System.Guid.NewGuid(),
                    GameInfo = escoba_info,
                    GameInfoId = escoba_info.GameInfoId, 
                    GameSessionState = "running",
                    UserPlayers = new List<UserGameSession>(),
                    GameStates = new List<games.GameState>(),
                };
                context.DbSet<GameSession>()
                    .Add(gameSession);

                ugs = new UserGameSession {
                    UserGameSessionId = System.Guid.NewGuid(),
                    GameInfoId = escoba_info.GameInfoId,
                    GameSessionId = gameSession.GameSessionId,
                    GameSession = gameSession,
                    User = user_player,
                    UserId = user_player.UserId,
                };

                gameSession.GameStates
                    .Add(initial_game_state);
        
                // TODO - Is all this needed or some auto?
                context.DbSet<UserGameSession>()
                    .Add(ugs);
                // user_player.GameSessions
                //     .Add(ugs);
                // gameSession.UserPlayers
                //     .Add(ugs);

            } 
            // Lookup game session
            else {
                gameSession = context.DbSet<GameSession>()
                    .Find(sessionid);
                initial_game_state = gameSession.GameStates
                    .OrderBy(x => x.TurnCount)
                    .LastOrDefault();
                // Find current/logged-in user (url param)
                ugs = gameSession.UserPlayers
                    .Where(x => x.User.EmailAddress == userEmail)
                    .SingleOrDefault();
            }

            await context.SaveChangesAsync();

            var payload = new InitGamePayload {
                GameSessionId = gameSession.GameSessionId,
                GameState = initial_game_state,
            };
            return payload;
        }
    }
  }
}
