
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
        
      if (!context.Games.Any()) {
        context.Games.Add(new Models.GameInfo {
          GameInfoId = 1,
          GameName = "escoba",
          Rules = "The rules are TBD"
        });
      }

      if (!context.GameSessions.Any()) {
        context.GameSessions.Add( new Models.GameSession {
          GameSessionId = 1,
          GameSessionState = "done",
          SelectedGameInfoId = 1,

        });
      }
    
      // Look for any board games already in database.
      if (!context.Users.Any())
      {
        // TODO
        context.Users.Add(new Models.User {
          UserId = 321,
          FirstName = "John",
          LastName = "Doe",
          EmailAddress = "jdoe@acme.com",
          Stats = new List<Models.UserStats> {
            new Models.UserStats {
              Draws = 44,
              NumberOfPlays = 100,
              Losses = 23,
              Wins = 33,
              SelectedGameInfoId = 1 
          }}
          
        });
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


      modelBuilder.Entity<Models.GameSession>().Property(e => e.GameStates)
      .HasConversion(
        v => JsonSerializer.Serialize<ICollection<games.GameState>>(v, new JsonSerializerOptions{AllowTrailingCommas = true, IgnoreNullValues = true}),
        v => JsonSerializer.Deserialize<ICollection<games.GameState>>(v, new JsonSerializerOptions {AllowTrailingCommas =true, IgnoreNullValues = true})
        //v => JsonSerializer.Serialize(v, typeof(games.GameState)),
        //v => JsonSerializer.Deserialize<System.Collections.Generic.IList< games.GameState>>(v)
      );
    }

    public DbSet<Models.GameSession> GameSessions { get; set; }
    public DbSet<Models.User> Users {get; set;}
    public DbSet<Models.GameInfo> Games {get; set;}

  }
}