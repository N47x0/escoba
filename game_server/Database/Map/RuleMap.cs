using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using game_server.Database.Models;
using game_server.Map;

namespace game_server.Database.Map
{
	public class RuleMap : BaseEntityMapy<Rule>
	{
		protected override void InternalMap(EntityTypeBuilder<Rule> builder)
		{
			builder
				.ToTable("Rules", schema: "Games");

			builder
				.HasKey(x => x.RuleId);
				// .HasName("PK_GameInfo");

			builder
				.HasIndex(x => x.GameInfoId);

			builder.Property<Guid>(x => x.RuleId)
				.HasColumnName("RuleId")
				.ValueGeneratedOnAdd()
				.HasColumnType("uniqueidentifier");

			builder
				.Property<string>(x => x.RuleName)
				.HasColumnName("RuleName")
				.HasColumnType("nvarchar(max)");
			builder
				.Property<string>(x => x.RuleText)
				.HasColumnName("RuleText")
				.HasColumnType("nvarchar(max)");
			builder
				.HasOne("game_server.Database.Models.GameInfo", null)
				.WithMany("Rules")
				.HasForeignKey("GameInfoId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}