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
                name: "GameSession",
                schema: "Games",
                columns: table => new
                {
                    GameSessionId = table.Column<Guid>(nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    GameSessionState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameStates = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSession", x => x.GameSessionId);
                    table.ForeignKey(
                        name: "FK_GameSession_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                schema: "Games",
                columns: table => new
                {
                    GameStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimesPlayed = table.Column<int>(type: "int", nullable: false),
                    HumanWins = table.Column<int>(type: "int", nullable: false),
                    AiWins = table.Column<int>(type: "int", nullable: false),
                    HumanLosses = table.Column<int>(type: "int", nullable: false),
                    AiLosses = table.Column<int>(type: "int", nullable: false),
                    HumanDraws = table.Column<int>(type: "int", nullable: false),
                    AiDraws = table.Column<int>(type: "int", nullable: false)
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
                    GameInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "UserStatistics",
                schema: "Games",
                columns: table => new
                {
                    UserStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfPlays = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.UserStatisticId);
                    table.ForeignKey(
                        name: "FK_UserStatistics_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Games",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGameSession",
                schema: "Games",
                columns: table => new
                {
                    UserGameSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameSession", x => x.UserGameSessionId);
                    table.ForeignKey(
                        name: "FK_UserGameSession_GameInfo_GameInfoId",
                        column: x => x.GameInfoId,
                        principalSchema: "Games",
                        principalTable: "GameInfo",
                        principalColumn: "GameInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGameSession_GameSession_GameSessionId",
                        column: x => x.GameSessionId,
                        principalSchema: "Games",
                        principalTable: "GameSession",
                        principalColumn: "GameSessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGameSession_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Games",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "GameInfo",
                columns: new[] { "GameInfoId", "GameName" },
                values: new object[,]
                {
                    { new Guid("667e6c24-5647-4ad4-b390-e36df9199fd9"), "Escoba" },
                    { new Guid("e221aa88-2cc0-4ebe-956f-baaa557d08f0"), "Pusoy Dos" }
                });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("e41da16a-b3f4-4b35-ac95-e96f443d0ab0"), "jdoe@acme.com", "John", "Doe" },
                    { new Guid("bf71e378-f830-4320-a96c-844647bca7a3"), "ai@escoba.com", "Hal", "9000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_GameInfoId",
                schema: "Games",
                table: "GameSession",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_GameInfoId",
                schema: "Games",
                table: "GameStatistics",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_GameInfoId",
                schema: "Games",
                table: "Rules",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSession_GameInfoId",
                schema: "Games",
                table: "UserGameSession",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSession_GameSessionId",
                schema: "Games",
                table: "UserGameSession",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameSession_UserId",
                schema: "Games",
                table: "UserGameSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_GameInfoId",
                schema: "Games",
                table: "UserStatistics",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                schema: "Games",
                table: "UserStatistics",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStatistics",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "Rules",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "UserGameSession",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "UserStatistics",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameSession",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameInfo",
                schema: "Games");
        }
    }
}
