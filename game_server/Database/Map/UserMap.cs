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
		}
	}
}