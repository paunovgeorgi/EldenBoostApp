using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Services_And_ServiceOptions_Seeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "ImageURL", "IsActive", "MaxAmount", "Price", "PurchaseCount", "ServiceType", "Title" },
                values: new object[,]
                {
                    { 1, "You will get a 100% game completion and all the items we aquire along the way!", "images/service/gameCompletion.jpg", true, 0, 300.00m, 0, 0, "Game Completion" },
                    { 2, "Select the boss you'd like us to defeat and our boosters will get the job done!", "images/service/bossKills.jpg", true, 0, 10.00m, 0, 2, "Boss Kills" },
                    { 3, "Select the desired amount of levels you'd like and we'll levep-up your character in no time!", "images/service/leveling.png", true, 100, 2.50m, 0, 1, "Leveling" },
                    { 4, "A professional Elden Ring player will giude you and coach you along the way of your journey. Simply select the amount of hours you'd like to purchase.", "images/service/coaching.jpg", true, 20, 10.00m, 0, 1, "Coaching" },
                    { 5, "We'll get you the selected amount of talismans!", "images/service/talismans.jpg", true, 100, 1.00m, 0, 1, "Talismans" },
                    { 6, "We will complete the Shadow of the Erdtree DLC for you!", "images/service/shadow.jpg", true, 0, 150.00m, 0, 0, "Shadow of the Erdtree" },
                    { 7, "We'll get you the selected amount of runes!", "images/service/runes.jpg", true, 100, 1.50m, 0, 1, "Runes" },
                    { 8, "We will complete the selected dungeon for you!", "images/service/dungeons.jpg", true, 0, 39.00m, 0, 2, "Dungeon Runs" }
                });

            migrationBuilder.InsertData(
                table: "ServiceOptions",
                columns: new[] { "Id", "Name", "Price", "ServiceId" },
                values: new object[,]
                {
                    { 1, "Margit the Fell Omen", 10.00m, 2 },
                    { 2, "Godrick the Grafted", 12.00m, 2 },
                    { 3, "Red Wolf of Radagon", 15.00m, 2 },
                    { 4, "Leonine Misbegotten", 15.00m, 2 },
                    { 5, "Malenia, Blade of Miquella", 25.00m, 2 },
                    { 6, "Stormveil Castle", 39.00m, 8 },
                    { 7, "Volcano Manor", 49.00m, 8 },
                    { 9, "Crumbling Farum Azula", 59.00m, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ServiceOptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
