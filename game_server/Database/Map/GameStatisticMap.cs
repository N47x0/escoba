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

			builder.Property<Guid>(x => x.GameStatisticId)
				.HasColumnName("GameStatisticId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<int>(x => x.AiWins)
				.HasColumnName("AiWins")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.AiDraws)
				.HasColumnName("AiDraws")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.AiLosses)
				.HasColumnName("AiLosses")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.HumanWins)
				.HasColumnName("HumanWins")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.HumanDraws)
				.HasColumnName("HumanDraws")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.HumanLosses)
				.HasColumnName("HumanLosses")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.TimesPlayed)
				.HasColumnName("TimesPlayed")
				.HasColumnType("int");
			builder
				.HasOne("game_server.Database.Models.GameInfo", null)
				.WithMany("GameStatistics")
				.HasForeignKey("GameInfoId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}