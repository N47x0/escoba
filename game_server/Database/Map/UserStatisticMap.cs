using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class UserStatisticMap : BaseEntityMapy<UserStatistic>
	{
		protected override void InternalMap(EntityTypeBuilder<UserStatistic> builder)
		{
			builder
				.ToTable("UserStatistics", schema: "Games");

			builder
				.HasKey(x => x.UserStatisticId);
        
			builder
				.HasIndex(x => x.GameInfoId);

			builder
				.HasIndex(x => x.UserId);

			builder.Property<Guid>(x => x.UserStatisticId)
				.HasColumnName("UserStatisticId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<int>(x => x.Wins)
				.HasColumnName("Wins")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.Draws)
				.HasColumnName("Draws")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.Losses)
				.HasColumnName("Losses")
				.HasColumnType("int");
			builder
				.Property<int>(x => x.NumberOfPlays)
				.HasColumnName("NumberOfPlays")
				.HasColumnType("int");
			builder
				.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
				.WithMany("UserStatistics")
				.HasForeignKey("GameInfoId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			builder
				.HasOne("game_server.Database.Models.User", "User")
				.WithMany("UserStatistics")
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}