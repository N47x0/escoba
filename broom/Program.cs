using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GameManager;

namespace broom
{
    public class Program
    {
        static void AutoGame () {
      
        Game g = new Game();
        Console.WriteLine("Starting a game of ESCOBA!");

        int rounds = 0;
        while (g.pl1.score < 15 && g.pl2.score < 15) {
            rounds++;
            if (rounds % 2 == 1) {
            g.PlayRound(g.pl1, g.pl2);
            } else {
            g.PlayRound(g.pl2, g.pl1);
            }
            Console.WriteLine($"Round {rounds}\t PL1: {g.pl1.score}\tPL2: {g.pl2.score}");
        }

    }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
