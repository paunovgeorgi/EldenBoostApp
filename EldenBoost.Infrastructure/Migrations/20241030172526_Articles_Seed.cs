using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Articles_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleType", "AuthorId", "Content", "ImageURL", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, 0, 1, "Elden Ring offers players a vast array of formidable foes, each more challenging than the last. In this article, we explore the toughest bosses that push players to their limits. Whether you're facing the relentless strikes of Malenia or the monstrous attacks of Radahn, we’ll dive into the bosses that have left a lasting impact on the community. Prepare to relive some of the most intense and rewarding encounters Elden Ring has to offer.", "images/articles/toughest-bosses.jpg", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toughest Bosses in Elden Ring" },
                    { 2, 1, 1, "In this article, we dive deep into the best weapons for every stage of Elden Ring. From the devastating power of colossal swords to the agility of twin blades, we cover a range of weapons to suit any playstyle. Each weapon offers a unique gameplay experience, and our guide will help you maximize your potential by selecting the best gear to fit your build and approach.", "images/articles/best-weapons.jpg", new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best Weapons in Elden Ring" },
                    { 3, 0, 2, "Elden Ring is a game rich with hidden secrets and subtle story elements. In this article, we bring you a curated list of Easter eggs and obscure details that you might have overlooked in your journey through the Lands Between. From unique NPC dialogues to hidden locations and items, discover the hidden lore and secrets that make the world of Elden Ring come alive in unexpected ways.", "images/articles/secrets.jpg", new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring: Secrets You Might Have Missed" },
                    { 4, 0, 2, "The latest patch has introduced some significant changes in Elden Ring. This article breaks down the most impactful updates, from balancing tweaks to bug fixes and new features. We'll go over which changes will affect players the most and what new content you can look forward to in this updated version of the game.", "images/articles/patch-notes.jpg", new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring: Patch Notes Breakdown" },
                    { 5, 1, 1, "Malenia is one of the most challenging encounters in Elden Ring, known for her devastating attacks and regenerating health. This guide provides detailed strategies for overcoming her attacks and defeating her. We'll cover the best equipment, skills, and techniques to maximize your chances against this formidable foe. With careful preparation and the right tactics, you can emerge victorious from one of the hardest fights in the game.", "images/articles/malenia.jpg", new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "How to Beat Malenia, Blade of Miquella" },
                    { 6, 1, 2, "This article delves into the best character builds for various playstyles, from powerful melee warriors to skilled sorcerers. Whether you're seeking the ultimate PvE powerhouse or a versatile PvP build, we cover all the options to help you create a character that suits your preferred approach. Get ready to take on any challenge in the Lands Between with these optimized builds.", "images/articles/best-builds.jpg", new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring: The Best Builds for Every Playstyle" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
