using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUsers_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1291e6cd-1aac-4f8a-af4d-4980f64aff27", 0, "8d933a88-cf6d-4eed-9283-d65dc1b66fdd", "theone@client.com", true, false, null, "Mr. Anderson", "THEONE@CLIENT.COM", "THEONE@CLIENT.COM", "AQAAAAIAAYagAAAAEKmOHl901Keto+EDlNrGV+3Bd65wVZMikV8QiEaXQYbxyjymphIXWr964/zf8m2pUg==", null, false, "images/clients/theone.jpg", "633858e2-0b3e-40d8-afc8-937e0f2059f8", false, "theone@client.com" },
                    { "2eff52b0-3277-456f-ac78-34553d260ac6", 0, "8ae8aa4f-7c09-4eda-8fd7-5f546422a398", "thebat@client.com", true, false, null, "Master Wayne", "THEBAT@CLIENT.COM", "THEBAT@CLIENT.COM", "AQAAAAIAAYagAAAAEGRG5lWQBVQbmLC8dlKxvMme5SbQo8SYQGlhh2Jwe1u22mVRApN48y0sw2lEcNRNjA==", null, false, "images/clients/thebat.jpg", "2a75aebe-505d-4198-893e-f2c2248b14c7", false, "thebat@client.com" },
                    { "362456a6-e52a-4065-a619-7af22c96e1e1", 0, "a4765a83-28a1-4b5c-b77e-1e54d09817f2", "obiwan@author.com", true, false, null, "Master Kenobi", "OBIWAN@AUTHOR.COM", "OBIWAN@AUTHOR.COM", "AQAAAAIAAYagAAAAECht4X81rdV7zlG1J1U83mkb8j5avcPMSmeqQOAzH78QK+UhB147uTss9ybcKbB4wg==", null, false, "images/authors/obiwan.jpg", "8c080d07-977e-47ed-931e-17e2a2b47485", false, "obiwan@author.com" },
                    { "54cf5237-4a7c-4050-9570-7cb5cb753aa5", 0, "ea12d05f-ab3a-4b4c-883e-e9520e9f6a58", "dva@booster.com", true, false, null, "D.VA", "DVA@BOOSTER.COM", "DVA@BOOSTER.COM", "AQAAAAIAAYagAAAAEHqGpIZPGwgqgBWlzYJhkwqcVChgLy6BUPMpsnLp7Q9XjweQSpzbrQnXcmGpJCX7rQ==", null, false, "images/boosters/dva.jpg", "a9ba2b72-c4f5-4959-8f78-1614e422216a", false, "dva@booster.com" },
                    { "851792db-67bb-4b08-8c03-ac2643a0600a", 0, "169a3cc9-b467-4a48-9312-3b6567671589", "quigon@author.com", true, false, null, "Qui-Gon-Jinn", "QUIGON@AUTHOR.COM", "QUIGON@AUTHOR.COM", "AQAAAAIAAYagAAAAEOKcHBhU7sn0REw0crkq6AiBZBjCWzh/oZ05EyFoMVL/3MuyVp+NJQIVf6Mn2789tg==", null, false, "images/authors/quigon.jpg", "9a8e05d8-8ae9-4932-8530-445c15f6815e", false, "quigon@author.com" },
                    { "871505e9-3338-487d-8496-760de2e1f2c2", 0, "1e40cd0a-db13-4eef-8fff-019c25950638", "dante@client.com", true, false, null, "Dante", "DANTE@CLIENT.COM", "DANTE@CLIENT.COM", "AQAAAAIAAYagAAAAEHpYQP2qmlF8npVgY2+F7YWwZhd9eGLlbjQJJqrJViH4Q61LLyVcd0iq7kH5F+8rPg==", null, false, "images/clients/dante.jpeg", "4c0881ea-45fc-48be-a3e0-37e78d6a159a", false, "dante@client.com" },
                    { "97e8127d-abaf-4980-9938-e388453fcbb4", 0, "eeeb1f5c-886b-42e3-86d3-d6cdbf63b679", "leon@client.com", true, false, null, "Leon S. Kennedy", "LEON@CLIENT.COM", "LEON@CLIENT.COM", "AQAAAAIAAYagAAAAEMtBp8r1eZwdSCtAMmMg9W7M7skaf8bJ73/Nrgkqg5DBOah8dMsttlE+aun0P/68KQ==", null, false, "images/clients/leon.jpg", "427511bc-68ca-4611-9c90-6d2562619b44", false, "leon@client.com" },
                    { "a13592d9-c4d0-4184-a3f9-dc7c66640808", 0, "2968e5ca-c1f9-490d-b1b4-300d8c82adb9", "cloud@booster.com", true, false, null, "Cloud", "CLOUD@BOOSTER.COM", "CLOUD@BOOSTER.COM", "AQAAAAIAAYagAAAAEEBrGQs9g09QRDHacTl8NvuZ/qhKkJrm6QXdGC8EYF8uHxAyWtQ0w517NcfYKdmoBw==", null, false, "images/boosters/cloud.jpg", "8f722837-0b3f-4ff0-b176-e8e05ae8d78e", false, "cloud@booster.com" },
                    { "b4e77aa9-36ad-4010-987e-9fd6d7b0d6ac", 0, "02c90fd2-4d83-40c1-abce-877426bad54c", "heisenberg@booster.com", true, false, null, "Mr. White", "HEISENBERG@BOOSTER.COM", "HEISENBERG@BOOSTER.COM", "AQAAAAIAAYagAAAAEHS23U0qkYQn3xJ2uZx3+Uxn6GVkBc5oXWo3SxZg/WkHLnKDcEHdPVSQCkho816Xuw==", null, false, "images/boosters/heisenberg.jpg", "ff1fc49c-bd0f-4918-b143-01f645c84a4d", false, "heisenberg@booster.com" },
                    { "ca3439dd-d67e-4733-8b72-a497af8b4c64", 0, "50b22881-ebaa-4493-8625-274465cc3620", "johnwick@booster.com", true, false, null, "Mr. Wick", "JOHNWICK@BOOSTER.COM", "JOHNWICK@BOOSTER.COM", "AQAAAAIAAYagAAAAEOmBVXKsTWaJFNG5OfVSes4eyN/UPwMXjIzb0xBVfQHbqsstMWGcbFJhtvXC5WSduw==", null, false, "images/boosters/john-wick.jpg", "885c560a-8788-4015-a2f5-a00995418226", false, "johnwick@booster.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1291e6cd-1aac-4f8a-af4d-4980f64aff27");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2eff52b0-3277-456f-ac78-34553d260ac6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "362456a6-e52a-4065-a619-7af22c96e1e1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54cf5237-4a7c-4050-9570-7cb5cb753aa5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "851792db-67bb-4b08-8c03-ac2643a0600a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "871505e9-3338-487d-8496-760de2e1f2c2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97e8127d-abaf-4980-9938-e388453fcbb4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a13592d9-c4d0-4184-a3f9-dc7c66640808");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4e77aa9-36ad-4010-987e-9fd6d7b0d6ac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca3439dd-d67e-4733-8b72-a497af8b4c64");
        }
    }
}
