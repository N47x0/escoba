using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class UserGameSessionMap : BaseEntityMapy<UserGameSession>
	{
		protected override void InternalMap(EntityTypeBuilder<UserGameSession> builder)
		{
			builder
				.ToTable("UserGameSession", schema: "Games");

			builder
				.HasKey(x => x.UserGameSessionId);
				// .HasName("PK_GameInfo");
        
			builder
				.HasIndex(x => x.GameInfoId);

			builder
				.HasIndex(x => x.GameSessionId);

			builder
				.HasIndex(x => x.UserId);
				// .HasName("PK_GameInfo");

			builder.Property<Guid>(x => x.UserGameSessionId)
				.HasColumnName("UserGameSessionId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			// builder.Property<Guid>(x => x.GameInfoId)
			// 	.HasColumnName("GameInfoId")
			// 	.HasColumnType("uniqueidentifier");

			builder.Property<Guid>(x => x.GameSessionId)
				.HasColumnName("GameSessionId")
				.HasColumnType("uniqueidentifier");

			builder.Property<Guid>(x => x.UserId)
				.HasColumnName("UserId")
				.HasColumnType("uniqueidentifier");
			// builder
			// 	.HasOne("game_server.Database.Models.GameInfo", null)
			// 	.WithMany("GameSessions")
			// 	.HasForeignKey("GameInfoId");
			builder
				.HasOne("game_server.Database.Models.GameSession", "GameSession")
				.WithMany("UserPlayers")
				.HasForeignKey("GameSessionId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			// builder
			// 	.HasOne("game_server.Database.Models.User", "User")
			// 	.WithMany("GameSessions")
			// 	.HasForeignKey("UserId")
			// 	.OnDelete(DeleteBehavior.Restrict)
			// 	.IsRequired();
		}
	}
}