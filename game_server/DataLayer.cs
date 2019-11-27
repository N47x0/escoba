
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace game_server
{
  public class DataLayer
  {
    public static void Initialize(GameSessionModelDBContext context, string approotpath)
    {
      var escoba_info = new Models.GameInfo {
          GameInfoId = System.Guid.NewGuid(),
          GameName = "escoba",
          Rules = "The rules are TBD",
        };
      // Look for any board games already in database.
      if (!context.Games.Any()) {
        context.Games.Add(escoba_info);
      }

      Models.GameSession gameSession;
      if (!context.GameSessions.Any()) {
        gameSession = new Models.GameSession {
          //GameSessionId = 1, auto gen? conf
          GameSessionId = System.Guid.Empty,
          GameSessionState = "done",
          SelectedGameInfoId = escoba_info.GameInfoId,
          GameStates = new List<games.GameState>{},
          UserPlayers = new List<Models.UserGameSession>{},
          // selected game info - auto linked?
        };
        context.GameSessions.Add( gameSession );
      } else {
        gameSession = context.GameSessions.First();
      }
    
      Models.User johnD;
      Models.User hal;
      if (!context.Users.Any())
      {
        // TODO
        johnD = new Models.User {
          UserId = System.Guid.NewGuid(),
          FirstName = "John",
          LastName = "Doe",
          EmailAddress = "jdoe@acme.com",
          Stats = new List<Models.UserStats> {
            new Models.UserStats {
              Draws = 44,
              NumberOfPlays = 100,
              Losses = 23,
              Wins = 33,
              SelectedGameInfoId = escoba_info.GameInfoId,
          }},
          GameSessions = new List<Models.UserGameSession>{},
        };
        context.Users.Add(johnD);

        hal = new Models.User {
          UserId = System.Guid.NewGuid(),
          FirstName = "Hal",
          LastName = "9000",
          EmailAddress = "ai@escoba.com",
          Stats = new List<Models.UserStats> {
            new Models.UserStats {
              Draws = 44,
              NumberOfPlays = 100,
              Losses = 33,
              Wins = 23,
              SelectedGameInfoId = escoba_info.GameInfoId,
          }},
          GameSessions = new List<Models.UserGameSession>{},
        };
        context.Users.Add(hal);
      } else {
        johnD = context.Users.Where(x => x.EmailAddress == "jdoe@acme.com").Single();
        hal = context.Users.Where(x => x.EmailAddress == "ai@escoba.com").Single();
      }

      // Seed user-gamesession join/association      
      Models.UserGameSession ugs;
      if (!context.GameSessions.Any()) {
        ugs = new Models.UserGameSession {
          GameSessionId = gameSession.GameSessionId,
          UserId = johnD.UserId,
        };
        gameSession.UserPlayers.Add(ugs);
        johnD.GameSessions.Add(ugs);
        context.GameSessions.Add(gameSession);
      }

      context.SaveChanges();
      return;   // Database has been seeded
    }
  }

  public class GameSessionModelDBContext : DbContext {
    public GameSessionModelDBContext(DbContextOptions<GameSessionModelDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // TODO - Move to separate EntityConfiguration Class
      modelBuilder.Entity<Models.UserGameSession>()
        .HasKey(ugs => new { ugs.UserId, ugs.GameSessionId });  
      modelBuilder.Entity<Models.UserGameSession>()
        .HasOne(ugs => ugs.User)
        .WithMany(u => u.GameSessions)
        .HasForeignKey(ugs => ugs.GameSessionId);  
      modelBuilder.Entity<Models.UserGameSession>()
        .HasOne(ugs => ugs.GameSession)
        .WithMany(gs => gs.UserPlayers)
        .HasForeignKey(ugs => ugs.UserId);

      // Shouldn't be needed - by convention links
      // modelBuilder.Entity<Models.User>()
      //   .HasMany(u => u.Stats)
      //   .WithOne(us => us.User);

      // TODO - Move to separate EntityConfiguration Class
      modelBuilder.Entity<Models.GameSession>().Property(e => e.GameStates)
      .HasConversion(
        v => JsonSerializer.Serialize<ICollection<games.GameState>>(v, new JsonSerializerOptions{AllowTrailingCommas = true, IgnoreNullValues = true}),
        v => JsonSerializer.Deserialize<ICollection<games.GameState>>(v, new JsonSerializerOptions {AllowTrailingCommas =true, IgnoreNullValues = true}));

      modelBuilder.Entity<Models.GameSession>().Property(e => e.GameSessionId)
      .ValueGeneratedOnAdd();
    }

    public DbSet<Models.GameSession> GameSessions { get; set; }
    public DbSet<Models.User> Users {get; set;}
    public DbSet<Models.GameInfo> Games {get; set;}

  }
}