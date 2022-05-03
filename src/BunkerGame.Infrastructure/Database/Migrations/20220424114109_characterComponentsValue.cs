using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BunkerGame.Infrastructure.Migrations
{
    public partial class characterComponentsValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Traits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Professions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Phobias",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Hobbies",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Healths",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "CharacterItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Cards",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "AdditionalInformations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Traits");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Phobias");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Healths");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CharacterItems");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "AdditionalInformations");
        }
    }
}
