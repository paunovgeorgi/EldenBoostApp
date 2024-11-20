using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsDemotedFlag_AddedTo_BoosterAndAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDemoted",
                table: "Boosters",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag for Active and Demoted boosters.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDemoted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag for Active and Demoted authors.");


            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDemoted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDemoted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Boosters",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDemoted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Boosters",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDemoted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Boosters",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDemoted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Boosters",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDemoted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDemoted",
                table: "Boosters");

            migrationBuilder.DropColumn(
                name: "IsDemoted",
                table: "Authors");
        }
    }
}
