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
                .Include(e => e.UserGameSessions)        
                .Where(x => x.EmailAddress == userEmail)
                .SingleOrDefault();

            var ai_player = context.DbSet<User>()
                .Include(e => e.UserStatistics)
                .Include(e => e.UserGameSessions)        
                .Where(x => x.EmailAddress == "ai@escoba.com")
                .SingleOrDefault();

            // TODO - Specific gamename resolution
            var escoba_info = context.DbSet<GameInfo>()
                .Where(x => x.GameName == "escoba")
                .SingleOrDefault();

            var initial_game_state = _cardGameService
                .InitGame();
            GameStatistic playerGameStatistic;
            GameStatistic aiGameStatistic;
            GameSession gameSession;
            UserGameSession ugsPlayer;
            UserGameSession ugsAi;
            // Make New GameSession
            if (sessionid == System.Guid.Empty) {
                Guid newGameSessionId = Guid.NewGuid();
                gameSession = new GameSession {
                    GameSessionId = newGameSessionId,
                    GameInfo = escoba_info,
                    GameInfoId = escoba_info.GameInfoId, 
                    GameSessionState = "running",
                    UserGameSessions = new List<UserGameSession>(),
                    GameStates = new List<games.GameState>(),
                };
                Guid playerGameStatisticId = Guid.NewGuid();
                playerGameStatistic = new GameStatistic {
                    GameStatisticId = playerGameStatisticId,
                    GameInfoId = escoba_info.GameInfoId,
                    GameSessionId = newGameSessionId,
                    UserStatisticId = user_player.UserStatistics.Where(us => us.GameInfoId == escoba_info.GameInfoId).FirstOrDefault().UserStatisticId,
                    UserStatistic = user_player.UserStatistics.Where(us => us.GameInfoId == escoba_info.GameInfoId).FirstOrDefault(),
                    UserGameSessions = new List<UserGameSession>(),
                    FinalScore = null,
                    HumanWin = null,
                    AiWin = null,
                    Draw = null,
                    // GameComplete = false,
                    GameStart = DateTime.Now,
                    GameEnd = null
                };

                Guid aiGameStatisticId = Guid.NewGuid();
                aiGameStatistic = new GameStatistic {
                    GameStatisticId = aiGameStatisticId,
                    GameInfoId = escoba_info.GameInfoId,
                    GameSessionId = newGameSessionId,
                    UserStatisticId = ai_player.UserStatistics.Where(us => us.GameInfoId == escoba_info.GameInfoId).FirstOrDefault().UserStatisticId,
                    UserStatistic = ai_player.UserStatistics.Where(us => us.GameInfoId == escoba_info.GameInfoId).FirstOrDefault(),
                    UserGameSessions = new List<UserGameSession>(),
                    FinalScore = null,
                    HumanWin = null,
                    AiWin = null,
                    Draw = null,
                    // GameComplete = false,
                    GameStart = DateTime.Now,
                    GameEnd = null
                };

                context.DbSet<GameSession>()
                    .Add(gameSession);
                context.DbSet<GameStatistic>()
                    .Add(playerGameStatistic);
                context.DbSet<GameStatistic>()
                    .Add(aiGameStatistic);

                ugsPlayer = new UserGameSession {
                    UserGameSessionId = Guid.NewGuid(),
                    GameInfoId = escoba_info.GameInfoId,
                    GameSessionId = gameSession.GameSessionId,
                    GameSession = gameSession,
                    User = user_player,
                    UserId = user_player.UserId,
                };
                ugsAi = new UserGameSession {
                    UserGameSessionId = Guid.NewGuid(),
                    GameInfoId = escoba_info.GameInfoId,
                    GameSessionId = gameSession.GameSessionId,
                    GameSession = gameSession,
                    User = ai_player,
                    UserId = ai_player.UserId,
                };

                gameSession.GameStates
                    .Add(initial_game_state);
        
                context.DbSet<UserGameSession>()
                    .Add(ugsPlayer);
                context.DbSet<UserGameSession>()
                    .Add(ugsAi);

            } 
            // Lookup game session
            else {
                gameSession = context.DbSet<GameSession>()
                    .Find(sessionid);
                initial_game_state = gameSession.GameStates
                    .OrderBy(x => x.TurnCount)
                    .LastOrDefault();
                // Find current/logged-in user (url param)
                ugsPlayer = gameSession.UserGameSessions
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
