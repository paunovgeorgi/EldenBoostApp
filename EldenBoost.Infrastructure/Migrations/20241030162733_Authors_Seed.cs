using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Authors_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Country", "UserId" },
                values: new object[,]
                {
                    { 1, "Tatooine", "362456a6-e52a-4065-a619-7af22c96e1e1" },
                    { 2, "Coruscant", "851792db-67bb-4b08-8c03-ac2643a0600a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
