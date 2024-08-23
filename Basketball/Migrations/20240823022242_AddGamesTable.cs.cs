using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basketball.Migrations
{
    /// <inheritdoc />
    public partial class AddGamesTablecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_PlayerId",
                table: "Statistics");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePlayed = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    HomeTeamID = table.Column<int>(type: "int", nullable: false),
                    AwayTeamID = table.Column<int>(type: "int", nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    WinnerTeamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTeams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTeams", x => new { x.GameId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_GameTeams_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_PlayerId",
                table: "Statistics",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_TeamId",
                table: "GameTeams",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTeams");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_PlayerId",
                table: "Statistics");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_PlayerId",
                table: "Statistics",
                column: "PlayerId");
        }
    }
}
