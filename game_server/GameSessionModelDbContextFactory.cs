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
  public interface IGameSessionModelDbContextFactory : IDesignTimeDbContextFactory<GameSessionModelDbContext>
  {

    // GameSessionModelDbContext CreateDbContext();
    GameSessionModelDbContext CreateDbContext(string[] args);
  }
  public class GameSessionModelDbContextFactory : IGameSessionModelDbContextFactory
  {
    private static string _connectionString;
    // public GameSessionModelDbContext CreateDbContext()
    // {
    //     return CreateDbContext(null);
    // }
    public GameSessionModelDbContext CreateDbContext(string[] args)
    {
        if (string.IsNullOrEmpty(_connectionString)) 
        {
            LoadConnectionString(args[0]);
        }
        var builder = new DbContextOptionsBuilder<GameSessionModelDbContext>();
        builder.UseSqlServer(_connectionString);
        return new GameSessionModelDbContext(builder.Options);
    }
    private static void LoadConnectionString(string initialCatalog)
    {
      var builder = new ConfigurationBuilder();
      builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
      var configuration = builder.Build();
      SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
      sb.InitialCatalog = initialCatalog;
      _connectionString = sb.ConnectionString;
    }
  }
}