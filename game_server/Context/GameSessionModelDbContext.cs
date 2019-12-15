using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using game_server.Database.Models;
using game_server.Map;
using game_server.Seed;


namespace game_server.Context
{
  public class GameSessionModelDbContextOptions
  {
    public readonly DbContextOptions<GameSessionModelDbContext> Options;
    public readonly IDbContextSeed DbContextSeed;
    public readonly IEnumerable<IEntityTypeMap> Mappings;
    public GameSessionModelDbContextOptions(DbContextOptions<GameSessionModelDbContext> options, 
                                            IDbContextSeed dbContextSeed, IEnumerable<IEntityTypeMap> mappings)
    {
      DbContextSeed = dbContextSeed;
      Options = options;
      Mappings = mappings;
    }

  }
  public class GameSessionModelDbContext : DbContext {
    private readonly GameSessionModelDbContextOptions options;
    public GameSessionModelDbContext(GameSessionModelDbContextOptions options)
        : base(options.Options)
    {
      this.options = options;
      
    }
    public GameSessionModelDbContext()
    {
    }
    static string targetDir = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
    public static readonly ILoggerFactory MyLoggerFactory
    = LoggerFactory.Create(builder =>
        {
            builder
                .AddConsole();
                // .AddFilter((category, level) =>
                //     category == DbLoggerCategory.Database.Command.Name
                //     && level == LogLevel.Information);
                // var standardOutput = new StreamWriter(Console.OpenStandardOutput());
                // standardOutput.AutoFlush = true;
                // Console.SetOut(standardOutput);
                // using (var sw = new StreamWriter(targetDir + @"\sql_output.txt")) 
                // {
                //   Console.SetOut(sw);
                // }
        });
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder
          .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      foreach (var mapping in options.Mappings)
      {
        mapping.Map(builder);
      }

      options.DbContextSeed?.Seed(builder);

      // if (System.Diagnostics.Debugger.IsAttached == false)
      // {
      //     System.Diagnostics.Debugger.Launch();
      // }
      // try 
      // {
      //   base.OnModelCreating(builder);

      //   foreach (var mapping in options.Mappings)
      //   {
      //     mapping.Map(builder);
      //   }

      //   options.DbContextSeed?.Seed(builder);
      // }
      // catch (Exception e)
      // {
      //   Console.WriteLine(e);
      //   Console.ReadLine();
      // }
    }
  }
  public static class GameSessionModelDbContextExtensions
  {
    public static DbSet<TEntityType> DbSet<TEntityType>(this GameSessionModelDbContext context)
      where TEntityType : class
      {
        return context.Set<TEntityType>();
      }
  }
}

