using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Design;

namespace game_server
{
  public class Startup
  {
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();
      // services.AddDbContext<GameSessionModelDbContext>(options => 
      //   options.UseInMemoryDatabase(databaseName: "LocalInMemory"));

      services.AddTransient<IGameSessionModelDbContextFactory, GameSessionModelDbContextFactory>();
      
      // services.AddTransient<IGameSessionModelDbContextFactory, GameSessionModelDbContextFactory>();

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

      services.AddScoped<games.ICardGame, games.escoba.EscobaCardGameImpl>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GameSessionModelDbContext game_session_ctx)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        DataLayer.Initialize(game_session_ctx, "");
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseCors(MyAllowSpecificOrigins);
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
