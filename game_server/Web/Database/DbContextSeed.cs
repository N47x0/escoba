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
			var escoba_info = new GameInfo {
				GameInfoId = Guid.NewGuid(),
				GameName = "Escoba"
				};
			var pusoydos_info = new GameInfo {
				GameInfoId = Guid.NewGuid(),
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
				UserGameSessions = new List<UserGameSession>{}
			};

			Guid halId = Guid.NewGuid();

			var hal = new User {
				UserId = halId,
				FirstName = "Hal",
				LastName = "9000",
				EmailAddress = "ai@escoba.com",
				UserStatistics = new List<UserStatistic> {},
				UserGameSessions = new List<UserGameSession>{}
			};

			modelBuilder.Entity<User>()
				.HasData(johnD, hal);

			var johnStats = new UserStatistic {
				UserStatisticId = Guid.NewGuid(),
				UserId = johnId,
				GameInfoId = escoba_info.GameInfoId,
				GameInfo = escoba_info,
				NumberOfPlays = 100,
				Wins = 33,
				Losses = 23,
				Draws = 44,
			};
			var halStats = new UserStatistic {
				UserStatisticId = Guid.NewGuid(),
				UserId = halId,
				GameInfoId = escoba_info.GameInfoId,
				GameInfo = escoba_info,
				NumberOfPlays = 100,
				Wins = 33,
				Losses = 23,
				Draws = 44,
			};

			// this was causing the migration to fail 
			// The type 'UserStatistic' cannot be configured as non-owned because an owned entity type with the same name already exists.

			// modelBuilder.Entity<UserStatistic>()
			// 	// .OwnsOne(us => us.GameInfoId)
			// 	.HasData(johnStats, halStats);

		}
	}
}