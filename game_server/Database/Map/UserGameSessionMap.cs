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
				.ToTable("UserGameSessions", schema: "Games");

			builder.Property<Guid>(x => x.UserGameSessionId)
				.HasColumnName("UserGameSessionId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.HasKey(x => x.UserGameSessionId);
        
			builder
				.HasIndex(x => x.GameInfoId);

			builder
				.HasIndex(x => x.GameSessionId);

			// builder
			// 	.HasIndex(x => x.GameStatisticId);

			builder
				.HasIndex(x => x.UserId);

			builder
				.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
				.WithMany("UserGameSessions")
				.HasForeignKey("GameInfoId");

			builder
				.HasOne("game_server.Database.Models.GameSession", "GameSession")
				.WithMany("UserGameSessions")
				.HasForeignKey("GameSessionId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			builder
				.HasOne("game_server.Database.Models.User", "User")
				.WithMany("UserGameSessions")
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
		}
	}
}