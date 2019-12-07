﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using game_server.Context;

namespace game_server.Migrations
{
    [DbContext(typeof(GameSessionModelDbContext))]
    partial class GameSessionModelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("game_server.Database.Models.GameInfo", b =>
                {
                    b.Property<Guid>("GameInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GameInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameName")
                        .HasColumnName("GameName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameInfoId");

                    b.ToTable("GameInfo","Games");

                    b.HasData(
                        new
                        {
                            GameInfoId = new Guid("667e6c24-5647-4ad4-b390-e36df9199fd9"),
                            GameName = "Escoba"
                        },
                        new
                        {
                            GameInfoId = new Guid("e221aa88-2cc0-4ebe-956f-baaa557d08f0"),
                            GameName = "Pusoy Dos"
                        });
                });

            modelBuilder.Entity("game_server.Database.Models.GameSession", b =>
                {
                    b.Property<Guid>("GameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameSessionState")
                        .HasColumnName("GameSessionState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameStates")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameSessionId");

                    b.HasIndex("GameInfoId");

                    b.ToTable("GameSession","Games");
                });

            modelBuilder.Entity("game_server.Database.Models.GameStatistic", b =>
                {
                    b.Property<Guid>("GameStatisticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GameStatisticId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AiDraws")
                        .HasColumnName("AiDraws")
                        .HasColumnType("int");

                    b.Property<int>("AiLosses")
                        .HasColumnName("AiLosses")
                        .HasColumnType("int");

                    b.Property<int>("AiWins")
                        .HasColumnName("AiWins")
                        .HasColumnType("int");

                    b.Property<Guid>("GameInfoId")
                        .HasColumnName("GameInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("HumanDraws")
                        .HasColumnName("HumanDraws")
                        .HasColumnType("int");

                    b.Property<int>("HumanLosses")
                        .HasColumnName("HumanLosses")
                        .HasColumnType("int");

                    b.Property<int>("HumanWins")
                        .HasColumnName("HumanWins")
                        .HasColumnType("int");

                    b.Property<int>("TimesPlayed")
                        .HasColumnName("TimesPlayed")
                        .HasColumnType("int");

                    b.HasKey("GameStatisticId");

                    b.HasIndex("GameInfoId");

                    b.ToTable("GameStatistics","Games");
                });

            modelBuilder.Entity("game_server.Database.Models.Rule", b =>
                {
                    b.Property<Guid>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameInfoId")
                        .HasColumnName("GameInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RuleName")
                        .HasColumnName("RuleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RuleText")
                        .HasColumnName("RuleText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RuleId");

                    b.HasIndex("GameInfoId");

                    b.ToTable("Rules","Games");
                });

            modelBuilder.Entity("game_server.Database.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnName("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnName("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnName("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users","Games");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("e41da16a-b3f4-4b35-ac95-e96f443d0ab0"),
                            EmailAddress = "jdoe@acme.com",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            UserId = new Guid("bf71e378-f830-4320-a96c-844647bca7a3"),
                            EmailAddress = "ai@escoba.com",
                            FirstName = "Hal",
                            LastName = "9000"
                        });
                });

            modelBuilder.Entity("game_server.Database.Models.UserGameSession", b =>
                {
                    b.Property<Guid>("UserGameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserGameSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameSessionId")
                        .HasColumnName("GameSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserGameSessionId");

                    b.HasIndex("GameInfoId");

                    b.HasIndex("GameSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGameSession","Games");
                });

            modelBuilder.Entity("game_server.Database.Models.GameSession", b =>
                {
                    b.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
                        .WithMany()
                        .HasForeignKey("GameInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("game_server.Database.Models.GameStatistic", b =>
                {
                    b.HasOne("game_server.Database.Models.GameInfo", null)
                        .WithMany("GameStatistics")
                        .HasForeignKey("GameInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("game_server.Database.Models.Rule", b =>
                {
                    b.HasOne("game_server.Database.Models.GameInfo", null)
                        .WithMany("Rules")
                        .HasForeignKey("GameInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("game_server.Database.Models.User", b =>
                {
                    b.OwnsMany("game_server.Database.Models.UserStatistic", "UserStatistics", b1 =>
                        {
                            b1.Property<Guid>("UserStatisticId")
                                .ValueGeneratedOnAdd()
                                .HasColumnName("UserStatisticId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Draws")
                                .HasColumnName("Draws")
                                .HasColumnType("int");

                            b1.Property<Guid>("GameInfoId")
                                .HasColumnName("GameInfoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Losses")
                                .HasColumnName("Losses")
                                .HasColumnType("int");

                            b1.Property<int>("NumberOfPlays")
                                .HasColumnName("NumberOfPlays")
                                .HasColumnType("int");

                            b1.Property<Guid>("UserId")
                                .HasColumnName("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Wins")
                                .HasColumnName("Wins")
                                .HasColumnType("int");

                            b1.HasKey("UserStatisticId");

                            b1.HasIndex("GameInfoId");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserStatistics","Games");

                            b1.HasOne("game_server.Database.Models.GameInfo", "GameInfo")
                                .WithMany()
                                .HasForeignKey("GameInfoId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner("User")
                                .HasForeignKey("UserId");
                        });
                });

            modelBuilder.Entity("game_server.Database.Models.UserGameSession", b =>
                {
                    b.HasOne("game_server.Database.Models.GameInfo", null)
                        .WithMany("GameSessions")
                        .HasForeignKey("GameInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("game_server.Database.Models.GameSession", "GameSession")
                        .WithMany("UserPlayers")
                        .HasForeignKey("GameSessionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("game_server.Database.Models.User", "User")
                        .WithMany("GameSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
