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
				.ToTable("GameSessions", schema: "Games");

			builder
				.HasKey(x => x.GameSessionId);
        
			builder
				.HasIndex(x => x.GameInfoId);

			builder
				.HasIndex(x => x.GameStatisticId);

			builder.Property<Guid>(x => x.GameSessionId)
				.HasColumnName("GameSessionId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<string>(x => x.GameSessionState)
				.HasColumnName("GameSessionState")
				.HasColumnType("nvarchar(max)");

			builder
				.Property<ICollection<games.GameState>>(x => x.GameStates)	
				.HasConversion(
				  v => JsonSerializer.Serialize<ICollection<games.GameState>>(v, new JsonSerializerOptions{AllowTrailingCommas = true, IgnoreNullValues = true}),
				  v => JsonSerializer.Deserialize<ICollection<games.GameState>>(v, new JsonSerializerOptions {AllowTrailingCommas =true, IgnoreNullValues = true}));
			builder
				.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
				.WithMany("GameSessions")
				.HasForeignKey("GameInfoId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			builder
				.HasOne("game_server.Database.Models.GameStatistic", "GameStatistic")
				.WithOne("GameSession")
				.HasForeignKey("GameStatistic")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
