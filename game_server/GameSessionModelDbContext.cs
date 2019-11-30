using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using game_server.Models;


namespace game_server
{
  public class GameSessionModelDbContext : DbContext {
    public GameSessionModelDbContext(DbContextOptions<GameSessionModelDbContext> options)
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
    public DbSet<Models.Rule> Rules { get; set; }
    public DbSet<Models.UserStatistic> UserStatistics { get; set; }

  }
}