using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using game_server.Context;
using game_server.Factory;

namespace game_server.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {

      var host = CreateHostBuilder(args).Build();
      using (var scope = host.Services.CreateScope())
      {
          var factory = scope.ServiceProvider.GetService<IGameSessionModelDbContextFactory>();
          var context = factory.CreateDbContext();
          context.Database.EnsureCreated();
          // context.Database.Migrate();
      }
      host.Run();

      // if (System.Diagnostics.Debugger.IsAttached == false)
      // {
      //     System.Diagnostics.Debugger.Launch();
      // }
      // try 
      // {
      //   var host = CreateHostBuilder(args).Build();

      //   using (var scope = host.Services.CreateScope())
      //   {
      //       var factory = scope.ServiceProvider.GetService<IGameSessionModelDbContextFactory>();
      //       var context = factory.CreateDbContext();
      //       context.Database.Migrate();
      //   }
      //   host.Run();
      // }
      // catch (Exception e)
      // {
      //   Console.WriteLine(e);
      //   Console.ReadLine();
      // }
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
          // if (System.Diagnostics.Debugger.IsAttached == false)
          // {
          //     System.Diagnostics.Debugger.Launch();
          // }
          // try 
          // {
          //   webBuilder.UseStartup<Startup>();
          // }
          // catch (Exception e)
          // {
          //   Console.WriteLine(e);
          //   Console.ReadLine();
          // }
        });
  }
}
