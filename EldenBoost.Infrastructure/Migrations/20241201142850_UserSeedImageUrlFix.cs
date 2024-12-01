using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EldenBoost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSeedImageUrlFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1291e6cd-1aac-4f8a-af4d-4980f64aff27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "1f4a16db-9a05-4a7c-bd42-463aecf2414e", "AQAAAAIAAYagAAAAELN1igkAelowzyB1vOe3/x/ViSKCNSA06pgRl+O6l7qYwf+lsEzdYvgDyEBs9rRd9Q==", "/images/clients/theone.jpg", "ba1259b3-1348-494a-a1dd-06591469c8b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2eff52b0-3277-456f-ac78-34553d260ac6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "c70c8614-b9ec-47a2-8e5b-95a9aa3afaa8", "AQAAAAIAAYagAAAAEJ3DcLoErdHt0z295rgPGMKSqcUjaiAy0S8UfDUGuVLPXSZdHKo5P1I2ZasMNU1yXQ==", "/images/clients/thebat.jpg", "58e4ee8a-8528-4b36-822b-226a93a590f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "362456a6-e52a-4065-a619-7af22c96e1e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "2e06a9e7-172e-4fbf-aaaf-3be5e461e818", "AQAAAAIAAYagAAAAED7gdBVChPdULcE7qyb6KsoOSixeDsrd+ixxD0jo8LuHne2zg2uYfYnCMqmxFXGoDw==", "/images/authors/obiwan.jpg", "58d4f330-1004-4ff8-9f18-12695fdc3acb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54cf5237-4a7c-4050-9570-7cb5cb753aa5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "8b9cf0b8-8318-43bc-ab00-5ac343ac6b15", "AQAAAAIAAYagAAAAEI8G6h/qQc1wQAU9McmyjcZqc50aPMjy1YJCxfcG6lV3NT9MzbfcUPHnepzs8kGnYg==", "/images/boosters/dva.jpg", "76d02b16-1308-4906-bc1b-7a2c520a8c3d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "698b474f-7790-4ae6-b186-a3ba3405bf99",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "f84520d2-f379-4da9-8037-22ffd1484671", "AQAAAAIAAYagAAAAEBYYJORBAfmhvb9OGQcAQk7DUQ5sgIa4tBRVeiGygLauFf/xo1kV5Dg3DkRxtvX1eA==", "/images/admin.jpg", "e3bc3397-6c5c-4d55-b753-42d0b16307a5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "851792db-67bb-4b08-8c03-ac2643a0600a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "adaff756-40ce-4b54-80c4-4a63b4fad1c3", "AQAAAAIAAYagAAAAEEyA3Om726LuAePa/kuJhr0J0i49zrL1OvydKkDSukMscLMX6tA2sTyU2VS2GLdmNw==", "/images/authors/quigon.jpg", "5afe3b67-ff3d-4c75-b736-1605bbcf9d1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "871505e9-3338-487d-8496-760de2e1f2c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "e18817f2-42fb-48c0-8a85-3cd1360782f5", "AQAAAAIAAYagAAAAEHkqRq/IznR8ikqI8EB2oiY2M/7ElLuqPpA45g4g6TOCJU4GiyBVg1PkuiNLp2pORw==", "/images/clients/dante.jpeg", "d7684fee-6f8e-47b4-8065-dad7db95ae7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97e8127d-abaf-4980-9938-e388453fcbb4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "27c01fa0-7242-4780-9670-1c4e6599a216", "AQAAAAIAAYagAAAAECv0Xd7vj/THWJzcOKk0qOf1SYvMMdDPEgGn1k1WYC3zheJw0gGtPOaLhow9ndS2lw==", "/images/clients/leon.jpg", "bc5f835a-b92a-4dc6-919f-37620d300716" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a13592d9-c4d0-4184-a3f9-dc7c66640808",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "2f34dc07-09ad-4eea-b041-73a8af45ab83", "AQAAAAIAAYagAAAAEGxt+jXHG0sTqit7RIPcCued+zbu+JXxMMFZwLP/FxCOLmim0P4bN+EUhwqpHiuZSA==", "/images/boosters/cloud.jpg", "42844154-e7a8-4c61-9ea4-575430c1aa7c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4e77aa9-36ad-4010-987e-9fd6d7b0d6ac",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "10bb4148-312f-44f6-8b7d-925ce4729000", "AQAAAAIAAYagAAAAECDh3tidjwEkPdx/syP78ZAr+QLkWHmFPGKNSDNsvyWo7XPsh3YBlCgXrm/vUdbD5g==", "/images/boosters/heisenberg.jpg", "c0249724-cb00-48d8-b3b4-237e0903e66f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca3439dd-d67e-4733-8b72-a497af8b4c64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "bcc4678f-9ef7-457c-8a76-6c3b9d81ce84", "AQAAAAIAAYagAAAAEHfMbKYG5/3IhK1DbIpS7bI+NGAOA8vqkrbbvVHek3TKYIt7eVDJ8nIDaSUZU1cG0A==", "/images/boosters/john-wick.jpg", "e2074b0b-b84e-4bdf-9fcc-6871b2e7b349" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1291e6cd-1aac-4f8a-af4d-4980f64aff27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "22217958-d199-4bff-a902-4ff481ddde01", "AQAAAAIAAYagAAAAECDhoPqESKW3IQ+FHv2GjllY6aQ1YTDQcizuv6uGp2RBYgFnqWl32Lijg4WtLRdw8w==", "images/clients/theone.jpg", "b2dc25f7-068b-485e-aa47-34e87f0ae8f5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2eff52b0-3277-456f-ac78-34553d260ac6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "098e5ee7-f6b1-4547-96b7-a7a72a4400fa", "AQAAAAIAAYagAAAAEP7bYJnwNlrhAyznh5+XJeKD4ssYcRLvREUC5hb9h/e/uC9fdeevF3kJnyiX3za6uw==", "images/clients/thebat.jpg", "3ff56fc0-3bfd-4446-824a-604112ae513d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "362456a6-e52a-4065-a619-7af22c96e1e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "3938d85a-dd36-4acd-9f74-ab9260d3ed73", "AQAAAAIAAYagAAAAEBbqIKkJwxIhR17HqIk2Dm0niqB0pLuxVR2IYPaUTedq18UNUR22TJV5h0xtGP/6+g==", "images/authors/obiwan.jpg", "e2c27c6b-0550-4ac0-ab71-45892831da28" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54cf5237-4a7c-4050-9570-7cb5cb753aa5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "8ae7e996-9ab7-4369-b384-009667269c53", "AQAAAAIAAYagAAAAEOCnXD460fDrSxVsbQk3p1umCQz+IEp50hvzuCZ0EgMg+mn/Yhs7Y7LKqVJnsLD5cA==", "images/boosters/dva.jpg", "c6369638-7c41-4e26-8301-98e73dced5ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "698b474f-7790-4ae6-b186-a3ba3405bf99",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "875389d7-74d1-4c6e-b4e8-30634b7d8484", "AQAAAAIAAYagAAAAEIExxe6WwJxE9wdRoIzM7r/Mn8ReSflwYQfnAwbbL5IG+dmqjerZn79EqSzSu07rXQ==", "images/admin.jpg", "5f516aaf-af7a-43e2-8522-dcdf6f502c5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "851792db-67bb-4b08-8c03-ac2643a0600a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "efcb6c07-9e92-420c-9ce9-e7740c494871", "AQAAAAIAAYagAAAAED5svfZya8RXxqhgtAKuEPWHTdBAf2KBjCnzNAfdKoA1cJOmvx3G+cww7h/WDNOUVA==", "images/authors/quigon.jpg", "188c4da1-dac5-45c3-bee5-8f6a5ef5580e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "871505e9-3338-487d-8496-760de2e1f2c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "b5699747-5ce8-4ce5-9d7d-dd189c0a02ff", "AQAAAAIAAYagAAAAEDuHrzN9eGOFOsgmS29EPhIubfmUTJI71yNGfewt21lptr7f27uOQxsvY8L6JuGKog==", "images/clients/dante.jpeg", "3d545c38-860d-48af-b2e5-b3ffc1b62314" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97e8127d-abaf-4980-9938-e388453fcbb4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "322338c6-54d2-4641-8076-f2df1233bd3f", "AQAAAAIAAYagAAAAEAQspYVEOjgs9lgXnqDYrGiYdssa4tAux5qLUhuIRb/Fidj9NmKoOLYWpPXukQSbyw==", "images/clients/leon.jpg", "2af52aa4-90a2-4f58-a6ee-6e89fd2b8942" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a13592d9-c4d0-4184-a3f9-dc7c66640808",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "8e344262-531a-4897-805f-16a526b8dc7a", "AQAAAAIAAYagAAAAEBh7hbwoqlG5FvUT0HO3q8a80ZSB/I+f2zzLe4ZWVrrD5zoIAHJrO4sCuEbHE/SXzw==", "images/boosters/cloud.jpg", "55aceed6-9563-401b-a9f7-3e9c38295f20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4e77aa9-36ad-4010-987e-9fd6d7b0d6ac",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "e702c6e8-0198-429c-98be-dfbbe9558468", "AQAAAAIAAYagAAAAEDjKGQv9cY35kbE2lf+Mut4FSPblTmVfGiHMpyiLlSqBZCeZiuVhZNrys3VPbxSyUA==", "images/boosters/heisenberg.jpg", "81af5079-0ddd-4d29-869d-e15197c1d021" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca3439dd-d67e-4733-8b72-a497af8b4c64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "a9d0cdb1-1ee3-43a6-9f68-ec874c76695b", "AQAAAAIAAYagAAAAEMsVfJlLVXUS2qC6ff8GyGS69kOjlNq8elR6nDhzBvxe4RdSbiu5XTRdIzJMAmlTLg==", "images/boosters/john-wick.jpg", "17c8ad61-6d18-4bf5-bc33-c0587546c944" });
        }
    }
}
