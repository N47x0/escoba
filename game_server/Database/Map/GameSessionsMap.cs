using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic; 
using System.Text.Json;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class GameSessionMap : BaseEntityMapy<GameSession>
	{
		protected override void InternalMap(EntityTypeBuilder<GameSession> builder)
		{
			builder
				.ToTable("GameSession", schema: "Games");

			builder
				.HasKey(x => x.GameSessionId);
				// .HasName("PK_GameInfo");
        
			builder
				.HasIndex(x => x.GameInfoId);
				// .HasName("PK_GameInfo");

			// builder.Property<Guid>(x => x.GameSessionId)
			// 	.HasColumnName("GameSessionId")
			// 	.ValueGeneratedOnAdd()
			// 	.HasColumnType("uniqueidentifier");

			// builder.Property<Guid>(x => x.GameInfoId)
			// 	.HasColumnName("GameInfoId")
			// 	.HasColumnType("uniqueidentifier");

			builder
				.Property<string>(x => x.GameSessionState)
				.HasColumnName("GameSessionState")
				.HasColumnType("nvarchar(max)");

			builder
				.Property<ICollection<games.GameState>>(x => x.GameStates)	
				.HasConversion(
				  v => JsonSerializer.Serialize<ICollection<games.GameState>>(v, new JsonSerializerOptions{AllowTrailingCommas = true, IgnoreNullValues = true}),
				  v => JsonSerializer.Deserialize<ICollection<games.GameState>>(v, new JsonSerializerOptions {AllowTrailingCommas =true, IgnoreNullValues = true}));

			// builder
			// 	.HasOne("game_server.Database.Models.GameInfo", null)
			// 	.WithMany()
			// 	.HasForeignKey("GameInfoId")
			// 	.OnDelete(DeleteBehavior.Restrict)
			// 	.IsRequired();
		}
	}
}

			// builder
			// 	.HasOne("game_server.Database.Models.GameInfo", null)
			// 	.WithMany("GameSessions")
			// 	.HasForeignKey("GameInfoId")
			// 	.OnDelete(DeleteBehavior.Restrict)
			// 	.IsRequired();
