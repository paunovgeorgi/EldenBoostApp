using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reviews_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "ReviewDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Amazing service! Got my Runes fast, and the booster was super friendly. Helped me clear some tough bosses I’ve been stuck on for weeks. Totally worth it! Will definitely use this service again. 10/10!", new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "2eff52b0-3277-456f-ac78-34553d260ac6" },
                    { 2, "Had an awesome experience! The boosting team was professional and kept me updated the whole time. Got all the items I wanted in no time. Highly recommended for anyone looking to enjoy Elden Ring without the grind", new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1291e6cd-1aac-4f8a-af4d-4980f64aff27" },
                    { 3, "I was skeptical at first, but the service was flawless. The booster got me through some difficult areas and my gear is maxed out now. Fast, safe, and affordable. If you need help, this is the place to go!", new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "97e8127d-abaf-4980-9938-e388453fcbb4" },
                    { 4, "I’ve been struggling with some of the harder bosses in Elden Ring, and this boost service saved me a ton of time. Professional, reliable, and quick delivery. If you’re stuck in the game, I highly recommend it!", new DateTime(2023, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "871505e9-3338-487d-8496-760de2e1f2c2" },
                    { 5, "The best boosting service out there! I needed some runes and boss kills, and they delivered everything faster than expected. The process was smooth, and my account was in safe hands. Will definitely come back!", new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "2eff52b0-3277-456f-ac78-34553d260ac6" },
                    { 6, "Fantastic job by the team! I got exactly what I asked for in a short time. The Play with a Pro option was especially fun – I learned so much. Super safe and legit, I couldn't have asked for a better experience.", new DateTime(2023, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "1291e6cd-1aac-4f8a-af4d-4980f64aff27" },
                    { 7, "Ordered a boosting service to get past some difficult bosses. The service was quick and efficient, and the booster communicated throughout. Completely satisfied with the results and highly recommend this service!", new DateTime(2023, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "97e8127d-abaf-4980-9938-e388453fcbb4" },
                    { 8, "A flawless service! Elden Ring is tough, but these guys made it easy. My character is leveled up, and I have all the gear I wanted. Great support, timely delivery, and no hassles. Can’t recommend them enough!", new DateTime(2023, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "871505e9-3338-487d-8496-760de2e1f2c2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
