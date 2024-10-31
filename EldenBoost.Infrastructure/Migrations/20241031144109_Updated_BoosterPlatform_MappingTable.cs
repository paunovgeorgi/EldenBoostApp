using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updated_BoosterPlatform_MappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoosterPlatform",
                columns: table => new
                {
                    BoosterId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoosterPlatform", x => new { x.BoosterId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_BoosterPlatform_Boosters_BoosterId",
                        column: x => x.BoosterId,
                        principalTable: "Boosters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoosterPlatform_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoosterPlatform_PlatformId",
                table: "BoosterPlatform",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "BoostersPlatforms",
                columns: table => new
                {
                    BoosterId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoostersPlatforms", x => new { x.BoosterId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_BoosterPlatform_Boosters_Id",
                        column: x => x.BoosterId,
                        principalTable: "Boosters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoosterPlatform_Platforms_Id",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_BoostersPlatforms_PlatformId",
                table: "BoostersPlatforms",
                column: "PlatformId");
        }
    }
}
