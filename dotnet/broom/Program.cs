using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Certificate;
using GameManager;

namespace broom
{
    public class Program
    {
        static void AutoGame () {
      
        Game g = new Game();
        Console.WriteLine("Starting a game of ESCOBA!");

        int rounds = 0;
        while (g.m_pl1.score < 15 && g.m_pl2.score < 15) {
            rounds++;
            if (rounds % 2 == 1) {
            g.PlayRound(g.m_pl1, g.m_pl2);
            } else {
            g.PlayRound(g.m_pl2, g.m_pl1);
            }
            Console.WriteLine($"Round {rounds}\t PL1: {g.m_pl1.score}\tPL2: {g.m_pl2.score}");
        }

    }

        public static void Main(string[] args)
        {

            //IServiceCollection

            //AutoGame();
            var host = CreateHostBuilder(args).Build(); 
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
