using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace game_server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Games");

            migrationBuilder.CreateTable(
                name: "GameInfo",
                schema: "Games",
                columns: table => new
                {
                    GameInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInfo", x => x.GameInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Games",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                schema: "Games",
                columns: table => new
                {
                    GameStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    GameSessionId = table.Column<Guid>(nullable: false),
                    FinalScore = table.Column<string>(nullable: true),
                    HumanWin = table.Column<bool>(type: "bit", nullable: false),
                    AiWin = table.Column<bool>(type: "bit", nullable: false),
                    Draw = table.Column<bool>(type: "bit", nullable: false),
                    GameStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.GameStatisticId);
                    table.ForeignKey(
                        name: "FK_GameStatistics_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                schema: "Games",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStatistic",
                columns: table => new
                {
                    UserStatisticId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    NumberOfPlays = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Draws = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistic", x => x.UserStatisticId);
                    table.ForeignKey(
                        name: "FK_UserStatistic_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStatistic_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Games",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSessions",
                schema: "Games",
                columns: table => new
                {
                    GameSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    GameStatisticId = table.Column<Guid>(nullable: false),
                    GameSessionState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameStates = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.GameSessionId);
                    table.ForeignKey(
                        name: "FK_GameSessions_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSessions_GameStatistics_GameStatisticId",
                        column: x => x.GameStatisticId,
                        principalSchema: "Games",
                        principalTable: "GameStatistics",
                        principalColumn: "GameStatisticId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGameSessions",
                schema: "Games",
                columns: table => new
                {
                    UserGameSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameSessionId = table.Column<Guid>(nullable: false),
                    GameStatisticId = table.Column<Guid>(nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameSessions", x => x.UserGameSessionId);
                    table.ForeignKey(
                        name: "FK_UserGameSessions_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGameSessions_GameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalSchema: "Games",
                        principalTable: "GameSessions",
                        principalColumn: "GameSessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGameSessions_GameStatistics_GameStatisticId",
                        column: x => x.GameStatisticId,
                        principalSchema: "Games",
                        principalTable: "GameStatistics",
                        principalColumn: "GameStatisticId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGameSessions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Games",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "GameInfo",
                columns: new[] { "GameInfoId", "GameName" },
                values: new object[,]
                {
                    { new Guid("84949e24-35ff-4edc-be57-6798e5bb14ff"), "Escoba" },
                    { new Guid("829094cb-c7bc-4bb1-92e3-e1ad5198ea94"), "Pusoy Dos" }
                });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("389ac3cf-70dd-4214-ad31-f7591ddec096"), "jdoe@acme.com", "John", "Doe" },
                    { new Guid("5464e256-4561-4759-8cb7-f3828f23fc73"), "ai@escoba.com", "Hal", "9000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistic_GameInfoId",
                table: "UserStatistic",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistic_UserId",
                table: "UserStatistic",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameInfoId",
                schema: "Games",
                table: "GameSessions",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameStatisticId",
                schema: "Games",
                table: "GameSessions",
                column: "GameStatisticId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_GameInfoId",
                schema: "Games",
                table: "GameStatistics",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_GameSessionId",
                schema: "Games",
                table: "GameStatistics",
                column: "GameSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rules_GameInfoId",
                schema: "Games",
                table: "Rules",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSessions_GameInfoId",
                schema: "Games",
                table: "UserGameSessions",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSessions_GameSessionId",
                schema: "Games",
                table: "UserGameSessions",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSessions_GameStatisticId",
                schema: "Games",
                table: "UserGameSessions",
                column: "GameStatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSessions_UserId",
                schema: "Games",
                table: "UserGameSessions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStatistic");

            migrationBuilder.DropTable(
                name: "Rules",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "UserGameSessions",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameSessions",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameStatistics",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameInfo",
                schema: "Games");
        }
    }
}
