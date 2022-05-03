using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BunkerGame.Infrastructure.Migrations
{
    public partial class ExternalSurrounding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalSurroundings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    SurroundingType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalSurroundings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalSurroundingGameSession",
                columns: table => new
                {
                    ExternalSurroundingsId = table.Column<int>(type: "integer", nullable: false),
                    GameSessionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalSurroundingGameSession", x => new { x.ExternalSurroundingsId, x.GameSessionsId });
                    table.ForeignKey(
                        name: "FK_ExternalSurroundingGameSession_ExternalSurroundings_Externa~",
                        column: x => x.ExternalSurroundingsId,
                        principalTable: "ExternalSurroundings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalSurroundingGameSession_GameSessions_GameSessionsId",
                        column: x => x.GameSessionsId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalSurroundingGameSession_GameSessionsId",
                table: "ExternalSurroundingGameSession",
                column: "GameSessionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalSurroundingGameSession");

            migrationBuilder.DropTable(
                name: "ExternalSurroundings");
        }
    }
}
