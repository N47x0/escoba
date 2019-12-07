using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class GameInfoMap : BaseEntityMapy<GameInfo>
	{
		protected override void InternalMap(EntityTypeBuilder<GameInfo> builder)
		{
			builder
				.ToTable("GameInfo", schema: "Games");

			builder
				.HasKey(x => x.GameInfoId);

			builder.Property<Guid>(x => x.GameInfoId)
				.HasColumnName("GameInfoId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<string>(x => x.GameName)
				.HasColumnName("GameName")
				.HasColumnType("nvarchar(max)");
				
			// builder
			// 	.HasMany("game_server.Database.Models.UserGameSession", "UserGameSession")
			// 	.WithOne("GameInfo");

		}
	}
}