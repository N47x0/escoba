using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using game_server.Context;
using game_server.Database.Map;
// using game_server.Web.Controllers;
using game_server.Web.Database;
using game_server.Factory;
using game_server.Map;
using game_server.Seed;

namespace game_server.Web
{
  public class Startup
  {
    public IWebHostEnvironment Environment { get; set; }
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public IConfiguration Configuration { get; }
    public Startup(IWebHostEnvironment env)
    {
      Environment = env;
      Configuration = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build()
      ;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // services.AddDbContext<GameSessionModelDbContext>(options => 
      //   options.UseInMemoryDatabase(databaseName: "LocalInMemory"));

      // services.AddTransient<IGameSessionModelDbContextFactory, GameSessionModelDbContextFactory>();
      
      // services.AddTransient<IGameSessionModelDbContextFactory, GameSessionModelDbContextFactory>();

      // var loggerFactory = new LoggerFactory();

      // loggerFactory.AddProvider(new )

      services.AddCors(options =>
      {
        options.AddDefaultPolicy( builder =>
        {
            builder.WithOrigins("http://localhost:8080",
                                "https://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
        options.AddPolicy(MyAllowSpecificOrigins, builder =>
        {
            builder.WithOrigins("http://example.com",
                                "http://localhost:8080",
                                "https://localhost:5001",
                                "http://192.168.1.106:8080",
                                "https://localhost:8080",
                                "https://192.168.1.106:8080").AllowAnyMethod().AllowAnyHeader();
        });
      });

      services.AddOptions();

      services.AddScoped<games.ICardGame, games.escoba.EscobaCardGameImpl>();
      services.AddScoped<IEntityTypeMap, GameInfoMap>();
      services.AddScoped<IEntityTypeMap, GameSessionMap>();
      services.AddScoped<IEntityTypeMap, GameStatisticMap>();
      services.AddScoped<IEntityTypeMap, UserMap>();
      services.AddScoped<IEntityTypeMap, RuleMap>();
      services.AddScoped<IEntityTypeMap, UserGameSessionMap>();
      // services.AddScoped<IEntityTypeMap, UserStatisticMap>();

      services.AddScoped<IDbContextSeed, DbContextSeed>();

      var dbContextOptions = new DbContextOptionsBuilder<GameSessionModelDbContext>()
        // .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("game_server.Migrations"))
        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
        .Options;

      services.AddSingleton(dbContextOptions);

      services.AddScoped<GameSessionModelDbContextOptions>();

      services.AddScoped<IGameSessionModelDbContextFactory, GameSessionModelDbContextFactory>();

      services.AddControllersWithViews();

      services.AddDbContext<GameSessionModelDbContext>();

      // if (System.Diagnostics.Debugger.IsAttached == false)
      // {
      //     System.Diagnostics.Debugger.Launch();
      // }
      // try 
      // {
      //   services.AddScoped<IGameSessionModelDbContextFactory, GameSessionModelDbContextFactory>();
      // }
      // catch (Exception e)
      // {
      //   Console.WriteLine(e);
      //   Console.ReadLine();
      // }


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GameSessionModelDbContext game_session_ctx)
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        // DataLayer.Initialize(game_session_ctx, "");
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseCors(MyAllowSpecificOrigins);
      app.UseHttpsRedirection();
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
            name: "default",
            // pattern: "{controller=Home}/{action=Index}/{id?}");
            pattern: "{controller}/{action}/{id}");
      });
    }
  }
}
