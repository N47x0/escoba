using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory;

namespace broom
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = "Host=localhost;Username=postgres;Password=password;Database=test";
            // var connString = "Host=localhost;Username=root;Password=password;Database=test";
            Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
            Console.WriteLine(Configuration["ConnectionStrings:DefaultConnection"]);
            Console.WriteLine(connString);
            services.AddDbContextPool<BroomDbContext>(options =>
            {
                options.UseMySql(connString,
                mySqlOptions => mySqlOptions.ServerVersion(new Version(8,0,15), ServerType.MySql));
            });
            //services.AddDbContext<ClientSessionDbContext>(options => options.UseInMemoryDatabase(databaseName: "ClientSessions"))
            //  services.AddDbContextPool<BroomDbContext>(options =>
            // {
            //     options.UseNpgsql(connString);
            // });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080",
                                            "https://localhost:5001")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://example.com",
                                            "http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
                    .AddCertificate();

            // All the other service configuration.
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseRouting();

            app.UseAuthentication();    

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("_myAllowSpecificOrigins");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
