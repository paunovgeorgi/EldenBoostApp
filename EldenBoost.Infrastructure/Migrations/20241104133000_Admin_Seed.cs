using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Admin_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "698b474f-7790-4ae6-b186-a3ba3405bf99", 0, "ce22a27b-4668-43b0-853b-59bb20776f9a", "admin@admin.com", true, false, null, "Admin", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEEuVTjX7VBK6Ne09/qH1nDRpPgxCwTyelR2CHD4Z+wNsEda+bGP9eHaT/l6B2i7ZtA==", null, false, "images/admin.jpg", "70e28e25-2fcd-4e13-a65a-65a88c5470a1", false, "admin@admin.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "698b474f-7790-4ae6-b186-a3ba3405bf99");
        }
    }
}
