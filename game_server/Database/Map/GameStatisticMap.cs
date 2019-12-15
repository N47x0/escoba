using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class GameStatisticMap : BaseEntityMapy<GameStatistic>
	{
		protected override void InternalMap(EntityTypeBuilder<GameStatistic> builder)
		{
			builder
				.ToTable("GameStatistics", schema: "Games");

			builder
				.HasKey(x => x.GameStatisticId);
        
			builder
				.HasIndex(x => x.GameInfoId);

			builder
				.HasIndex(x => x.GameSessionId);

			builder
				.HasIndex(x => x.UserStatisticId);


			builder.Property<Guid>(x => x.GameStatisticId)
				.HasColumnName("GameStatisticId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<bool?>(x => x.HumanWin)
				.HasColumnName("HumanWin")
				.HasColumnType("bit");
			builder
				.Property<bool?>(x => x.AiWin)
				.HasColumnName("AiWin")
				.HasColumnType("bit");
			builder
				.Property<bool?>(x => x.Draw)
				.HasColumnName("Draw")
				.HasColumnType("bit");
			builder
				.Property<bool>(x => x.GameComplete)
				.HasColumnName("GameComplete")
				.HasColumnType("bit");
			builder
				.Property<DateTime>(x => x.GameStart)
				.HasColumnName("GameStart")
				.HasColumnType("datetime2");
			builder
				.Property<DateTime?>(x => x.GameEnd)
				.HasColumnName("GameEnd")
				.HasColumnType("datetime2");
			builder
				.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
				.WithMany("GameStatistics")
				.HasForeignKey("GameInfoId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			builder
				.HasOne("game_server.Database.Models.UserStatistic", "UserStatistic")
				.WithMany("GameStatistics")
				.HasForeignKey("UserStatisticId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			builder
				.HasOne("game_server.Database.Models.GameSession", "GameSession")
				.WithOne("GameStatistic");
				// .HasForeignKey("GameSession", "GameSessionId");
			// 	.OnDelete(DeleteBehavior.Cascade)
			// 	.IsRequired();
		}
	}
}