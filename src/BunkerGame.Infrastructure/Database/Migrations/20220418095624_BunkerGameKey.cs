using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BunkerGame.Infrastructure.Migrations
{
    public partial class BunkerGameKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessions_Bunkers_BunkerId",
                table: "GameSessions");

            migrationBuilder.DropIndex(
                name: "IX_GameSessions_BunkerId",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "BunkerId",
                table: "GameSessions");

            migrationBuilder.CreateIndex(
                name: "IX_Bunkers_GameSessionId",
                table: "Bunkers",
                column: "GameSessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bunkers_GameSessions_GameSessionId",
                table: "Bunkers",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bunkers_GameSessions_GameSessionId",
                table: "Bunkers");

            migrationBuilder.DropIndex(
                name: "IX_Bunkers_GameSessionId",
                table: "Bunkers");

            migrationBuilder.AddColumn<int>(
                name: "BunkerId",
                table: "GameSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_BunkerId",
                table: "GameSessions",
                column: "BunkerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessions_Bunkers_BunkerId",
                table: "GameSessions",
                column: "BunkerId",
                principalTable: "Bunkers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
