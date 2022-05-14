using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BunkerGame.Infrastructure.Migrations
{
    public partial class bunkerSuppliesSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BunkerSize_Value",
                table: "Bunkers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Supplies_SuplliesYears",
                table: "Bunkers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BunkerSize_Value",
                table: "Bunkers");

            migrationBuilder.DropColumn(
                name: "Supplies_SuplliesYears",
                table: "Bunkers");
        }
    }
}
