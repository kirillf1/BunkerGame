using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BunkerGame.Infrastructure.Migrations
{
    public partial class freePlaceSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ChangedPlaceSize",
                table: "GameSessions",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "FreePlaceSize",
                table: "GameSessions",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedPlaceSize",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "FreePlaceSize",
                table: "GameSessions");
        }
    }
}
