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
                name: "GameSessions",
                schema: "Games",
                columns: table => new
                {
                    GameSessionId = table.Column<Guid>(nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                schema: "Games",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Rules_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Games",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserStatistics",
                schema: "Games",
                columns: table => new
                {
                    UserStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
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
                name: "GameStatistics",
                schema: "Games",
                columns: table => new
                {
                    GameStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    GameSessionId = table.Column<Guid>(nullable: false),
                    UserStatisticId = table.Column<Guid>(nullable: false),
                    FinalScore = table.Column<string>(nullable: true),
                    HumanWin = table.Column<bool>(type: "bit", nullable: true),
                    AiWin = table.Column<bool>(type: "bit", nullable: true),
                    Draw = table.Column<bool>(type: "bit", nullable: true),
                    GameComplete = table.Column<bool>(type: "bit", nullable: false),
                    GameStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_GameStatistics_GameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalSchema: "Games",
                        principalTable: "GameSessions",
                        principalColumn: "GameSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistics_UserStatistics_UserStatisticId",
                        column: x => x.UserStatisticId,
                        principalSchema: "Games",
                        principalTable: "UserStatistics",
                        principalColumn: "UserStatisticId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGameSessions",
                schema: "Games",
                columns: table => new
                {
                    UserGameSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameSessionId = table.Column<Guid>(nullable: false),
                    GameInfoId = table.Column<Guid>(nullable: false),
                    GameStatisticId = table.Column<Guid>(nullable: true),
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
                    { new Guid("c2ebe68a-6059-4351-8443-4491bc3bab48"), "Escoba" },
                    { new Guid("31616261-d554-4c90-9e8d-03bd49d8f56f"), "Pusoy Dos" }
                });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("3e74ec77-32a3-4a1e-ade6-9ab9afc9d37f"), "jdoe@acme.com", "John", "Doe" },
                    { new Guid("bd4953f8-6a6c-455c-8078-e8d07596008b"), "ai@escoba.com", "Hal", "9000" }
                });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "UserStatistics",
                columns: new[] { "UserStatisticId", "Draws", "GameInfoId", "Losses", "NumberOfPlays", "UserId", "Wins" },
                values: new object[] { new Guid("e03841ba-0c15-4844-82f3-3563b441a352"), 0, new Guid("c2ebe68a-6059-4351-8443-4491bc3bab48"), 0, 0, new Guid("3e74ec77-32a3-4a1e-ade6-9ab9afc9d37f"), 0 });

            migrationBuilder.InsertData(
                schema: "Games",
                table: "UserStatistics",
                columns: new[] { "UserStatisticId", "Draws", "GameInfoId", "Losses", "NumberOfPlays", "UserId", "Wins" },
                values: new object[] { new Guid("fb08cb85-86c2-4beb-bd29-11c010befb5a"), 0, new Guid("c2ebe68a-6059-4351-8443-4491bc3bab48"), 0, 0, new Guid("bd4953f8-6a6c-455c-8078-e8d07596008b"), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameInfoId",
                schema: "Games",
                table: "GameSessions",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameStatisticId",
                schema: "Games",
                table: "GameSessions",
                column: "GameStatisticId");

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
                name: "IX_GameStatistics_UserStatisticId",
                schema: "Games",
                table: "GameStatistics",
                column: "UserStatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_GameInfoId",
                schema: "Games",
                table: "Rules",
                column: "GameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_UserId",
                schema: "Games",
                table: "Rules",
                column: "UserId");

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
                name: "Rules",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "UserGameSessions",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameStatistics",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameSessions",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "UserStatistics",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "GameInfo",
                schema: "Games");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Games");
        }
    }
}
