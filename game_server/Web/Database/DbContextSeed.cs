using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using game_server.Database.Models;
using game_server.Seed;

namespace game_server.Web.Database
{
	public class DbContextSeed : IDbContextSeed
	{
		private readonly IConfiguration configuration;
		public DbContextSeed(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public void Seed(ModelBuilder modelBuilder)
		{
			// Add Customers:
			Guid escobaId = Guid.NewGuid();
			Guid pusoyId = Guid.NewGuid();
			var escoba_info = new GameInfo {
				GameInfoId = escobaId,
				GameName = "Escoba"
				};
			var pusoydos_info = new GameInfo {
				GameInfoId = pusoyId,
				GameName = "Pusoy Dos"
				};
			modelBuilder.Entity<GameInfo>()
				.HasData(escoba_info, pusoydos_info);

			Guid johnId = Guid.NewGuid();

			var johnD = new User {
				UserId = johnId,
				FirstName = "John",
				LastName = "Doe",
				EmailAddress = "jdoe@acme.com",
				UserStatistics = new List<UserStatistic> {},
				UserGameSessions = new List<UserGameSession>{},
			};

			Guid halId = Guid.NewGuid();

			var hal = new User {
				UserId = halId,
				FirstName = "Hal",
				LastName = "9000",
				EmailAddress = "ai@escoba.com",
				UserStatistics = new List<UserStatistic> {},
				UserGameSessions = new List<UserGameSession>{},
			};

			modelBuilder.Entity<User>()
				.HasData(johnD, hal);

			var johnStat = new UserStatistic {
				UserStatisticId = Guid.NewGuid(),
				UserId = johnId,
				GameInfoId = escobaId,
				NumberOfPlays = 0,
				Wins = 0,
				Losses = 0,
				Draws = 0,
				GameStatistics = new List<GameStatistic>{}
			};
			var halStat = new UserStatistic {
				UserStatisticId = Guid.NewGuid(),
				UserId = halId,
				GameInfoId = escobaId,
				NumberOfPlays = 0,
				Wins = 0,
				Losses = 0,
				Draws = 0,
				GameStatistics = new List<GameStatistic>{}
			};

			modelBuilder.Entity<UserStatistic>()
				.HasData(johnStat, halStat);


		}
	}
}