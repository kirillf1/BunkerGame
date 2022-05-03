using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BunkerGame.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "AdditionalInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddInfType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BunkerEnviroments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnviromentBehavior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EnviromentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunkerEnviroments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BunkerObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BunkerObjectType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunkerObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BunkerWalls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BunkerState = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunkerWalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardMethod_MethodType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CardMethod_MethodDirection = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CardMethod_ItemId = table.Column<int>(type: "integer", nullable: true),
                    IsSpecial = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catastrophes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CatastropheType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HidingTerm = table.Column<int>(type: "integer", nullable: false),
                    DestructionPercent = table.Column<short>(type: "smallint", nullable: false),
                    SurvivedPopulationPercent = table.Column<short>(type: "smallint", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catastrophes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterItemType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConversationName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WinGames = table.Column<long>(type: "bigint", nullable: false),
                    LostGames = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Healths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HealthType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Healths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HobbyType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemBunkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemBunkerType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBunkers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phobias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhobiaDebuffType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phobias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WinGames = table.Column<int>(type: "integer", nullable: false),
                    LoseGames = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TraitType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CatastropheId = table.Column<int>(type: "integer", nullable: false),
                    GameState = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSessions_Catastrophes_CatastropheId",
                        column: x => x.CatastropheId,
                        principalTable: "Catastrophes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardId = table.Column<int>(type: "integer", nullable: true),
                    ProfessionSkill = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProfessionType = table.Column<string>(type: "text", nullable: false),
                    CharacterItemId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsBalance = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professions_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Professions_CharacterItems_CharacterItemId",
                        column: x => x.CharacterItemId,
                        principalTable: "CharacterItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bunkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BunkerSize = table.Column<double>(type: "double precision", nullable: false),
                    GameSessionId = table.Column<long>(type: "bigint", nullable: true),
                    SuppliesYear = table.Column<int>(type: "integer", nullable: false),
                    BunkerWallId = table.Column<int>(type: "integer", nullable: false),
                    BunkerEnviromentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bunkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bunkers_BunkerEnviroments_BunkerEnviromentId",
                        column: x => x.BunkerEnviromentId,
                        principalTable: "BunkerEnviroments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bunkers_BunkerWalls_BunkerWallId",
                        column: x => x.BunkerWallId,
                        principalTable: "BunkerWalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bunkers_GameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: true),
                    CharacterNumber = table.Column<byte>(type: "smallint", nullable: false),
                    GameSessionId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionId = table.Column<int>(type: "integer", nullable: false),
                    Sex_Name = table.Column<string>(type: "text", nullable: false),
                    Age_Count = table.Column<int>(type: "integer", nullable: false),
                    ExperienceProfession = table.Column<byte>(type: "smallint", nullable: false),
                    Childbearing_CanGiveBirth = table.Column<bool>(type: "boolean", nullable: false),
                    IsAlive = table.Column<bool>(type: "boolean", nullable: false),
                    ExperienceHobby = table.Column<byte>(type: "smallint", nullable: false),
                    Size_Height = table.Column<double>(type: "double precision", nullable: false),
                    Size_Weight = table.Column<double>(type: "double precision", nullable: false),
                    HealthId = table.Column<int>(type: "integer", nullable: false),
                    TraitId = table.Column<int>(type: "integer", nullable: false),
                    PhobiaId = table.Column<int>(type: "integer", nullable: false),
                    HobbyId = table.Column<int>(type: "integer", nullable: false),
                    AdditionalInformationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_AdditionalInformations_AdditionalInformationId",
                        column: x => x.AdditionalInformationId,
                        principalTable: "AdditionalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_GameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Healths_HealthId",
                        column: x => x.HealthId,
                        principalTable: "Healths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Hobbies_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Phobias_PhobiaId",
                        column: x => x.PhobiaId,
                        principalTable: "Phobias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Traits_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Traits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BunkerBunkerObject",
                columns: table => new
                {
                    BunkerObjectsId = table.Column<int>(type: "integer", nullable: false),
                    BunkersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunkerBunkerObject", x => new { x.BunkerObjectsId, x.BunkersId });
                    table.ForeignKey(
                        name: "FK_BunkerBunkerObject_BunkerObjects_BunkerObjectsId",
                        column: x => x.BunkerObjectsId,
                        principalTable: "BunkerObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BunkerBunkerObject_Bunkers_BunkersId",
                        column: x => x.BunkersId,
                        principalTable: "Bunkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BunkerItemBunker",
                columns: table => new
                {
                    BunkersId = table.Column<int>(type: "integer", nullable: false),
                    ItemBunkersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunkerItemBunker", x => new { x.BunkersId, x.ItemBunkersId });
                    table.ForeignKey(
                        name: "FK_BunkerItemBunker_Bunkers_BunkersId",
                        column: x => x.BunkersId,
                        principalTable: "Bunkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BunkerItemBunker_ItemBunkers_ItemBunkersId",
                        column: x => x.ItemBunkersId,
                        principalTable: "ItemBunkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardCharacter",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "integer", nullable: false),
                    CharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCharacter", x => new { x.CardsId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_CardCharacter_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCharacter_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterCharacterItem",
                columns: table => new
                {
                    CharacterItemsId = table.Column<int>(type: "integer", nullable: false),
                    CharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCharacterItem", x => new { x.CharacterItemsId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterCharacterItem_CharacterItems_CharacterItemsId",
                        column: x => x.CharacterItemsId,
                        principalTable: "CharacterItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterCharacterItem_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsedCard",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    CardNumber = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCard", x => new { x.CharacterId, x.Id });
                    table.ForeignKey(
                        name: "FK_UsedCard_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BunkerBunkerObject_BunkersId",
                table: "BunkerBunkerObject",
                column: "BunkersId");

            migrationBuilder.CreateIndex(
                name: "IX_BunkerItemBunker_ItemBunkersId",
                table: "BunkerItemBunker",
                column: "ItemBunkersId");

            migrationBuilder.CreateIndex(
                name: "IX_Bunkers_BunkerEnviromentId",
                table: "Bunkers",
                column: "BunkerEnviromentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bunkers_BunkerWallId",
                table: "Bunkers",
                column: "BunkerWallId");

            migrationBuilder.CreateIndex(
                name: "IX_Bunkers_GameSessionId",
                table: "Bunkers",
                column: "GameSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardCharacter_CharactersId",
                table: "CardCharacter",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterCharacterItem_CharactersId",
                table: "CharacterCharacterItem",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AdditionalInformationId",
                table: "Characters",
                column: "AdditionalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GameSessionId",
                table: "Characters",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_HealthId",
                table: "Characters",
                column: "HealthId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_HobbyId",
                table: "Characters",
                column: "HobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PhobiaId",
                table: "Characters",
                column: "PhobiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlayerId",
                table: "Characters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ProfessionId",
                table: "Characters",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_TraitId",
                table: "Characters",
                column: "TraitId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_CatastropheId",
                table: "GameSessions",
                column: "CatastropheId");

            migrationBuilder.CreateIndex(
                name: "IX_Professions_CardId",
                table: "Professions",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Professions_CharacterItemId",
                table: "Professions",
                column: "CharacterItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BunkerBunkerObject");

            migrationBuilder.DropTable(
                name: "BunkerItemBunker");

            migrationBuilder.DropTable(
                name: "CardCharacter");

            migrationBuilder.DropTable(
                name: "CharacterCharacterItem");

            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.DropTable(
                name: "UsedCard");

            migrationBuilder.DropTable(
                name: "BunkerObjects");

            migrationBuilder.DropTable(
                name: "Bunkers");

            migrationBuilder.DropTable(
                name: "ItemBunkers");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "BunkerEnviroments");

            migrationBuilder.DropTable(
                name: "BunkerWalls");

            migrationBuilder.DropTable(
                name: "AdditionalInformations");

            migrationBuilder.DropTable(
                name: "GameSessions");

            migrationBuilder.DropTable(
                name: "Healths");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Phobias");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Traits");

            migrationBuilder.DropTable(
                name: "Catastrophes");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "CharacterItems");
        }
    }
}
