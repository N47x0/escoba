using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class UserMap : BaseEntityMapy<User>
	{
		protected override void InternalMap(EntityTypeBuilder<User> builder)
		{
			builder
				.ToTable("Users", schema: "Games");

			builder
				.HasKey(x => x.UserId);
				// .HasName("PK_GameInfo");

			builder.Property<Guid>(x => x.UserId)
				.HasColumnName("UserId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<string>(x => x.EmailAddress)
				.HasColumnName("EmailAddress")
				.HasColumnType("nvarchar(max)");
			builder
				.Property<string>(x => x.FirstName)
				.HasColumnName("FirstName")
				.HasColumnType("nvarchar(max)");
			builder
				.Property<string>(x => x.LastName)
				.HasColumnName("LastName")
				.HasColumnType("nvarchar(max)");
			builder
				.OwnsMany(u => u.UserStatistics, statBuilder =>
				{
					statBuilder
						.WithOwner();
					statBuilder
						.ToTable("UserStatistics", schema: "Games");

					statBuilder
						.HasKey(x => x.UserStatisticId);
						// .HasName("PK_GameInfo");
						
					statBuilder
						.HasIndex(x => x.GameInfoId);

					statBuilder
						.HasIndex(x => x.UserId);
						// .HasName("PK_GameInfo");

					statBuilder.Property<Guid>(x => x.UserStatisticId)
						.HasColumnName("UserStatisticId")
						.ValueGeneratedOnAdd()
						.HasColumnType("uniqueidentifier");

					statBuilder.Property<Guid>(x => x.GameInfoId)
						.HasColumnName("GameInfoId")
						.HasColumnType("uniqueidentifier");

					statBuilder.Property<Guid>(x => x.UserId)
						.HasColumnName("UserId")
						.HasColumnType("uniqueidentifier");

					statBuilder
						.Property<int>(x => x.Wins)
						.HasColumnName("Wins")
						.HasColumnType("int");
					statBuilder
						.Property<int>(x => x.Draws)
						.HasColumnName("Draws")
						.HasColumnType("int");
					statBuilder
						.Property<int>(x => x.Losses)
						.HasColumnName("Losses")
						.HasColumnType("int");
					statBuilder
						.Property<int>(x => x.NumberOfPlays)
						.HasColumnName("NumberOfPlays")
						.HasColumnType("int");
					statBuilder
						.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
						.WithMany()
						.HasForeignKey("GameInfoId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
					statBuilder
						.HasOne("game_server.Database.Models.User", "User")
						.WithMany("UserStatistics")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

				});
		}
	}
}