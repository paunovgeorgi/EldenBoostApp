using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public ApplicationUserConfiguration(IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(GenerateUsers());
        }

        private ApplicationUser[] GenerateUsers()
        {
            var users = new List<ApplicationUser>();

            var user1 = new ApplicationUser
            {
                Id = "a13592d9-c4d0-4184-a3f9-dc7c66640808",
                UserName = "cloud@booster.com",
                NormalizedUserName = "CLOUD@BOOSTER.COM",
                Email = "cloud@booster.com",
                NormalizedEmail = "CLOUD@BOOSTER.COM",
                EmailConfirmed = true,
                Nickname = "Cloud",
                ProfilePicture = "/images/boosters/cloud.jpg"
            };
            user1.PasswordHash = _passwordHasher.HashPassword(user1, "123456");

            users.Add(user1);

            var user2 = new ApplicationUser
            {
                Id = "b4e77aa9-36ad-4010-987e-9fd6d7b0d6ac",
                UserName = "heisenberg@booster.com",
                NormalizedUserName = "HEISENBERG@BOOSTER.COM",
                Email = "heisenberg@booster.com",
                NormalizedEmail = "HEISENBERG@BOOSTER.COM",
                EmailConfirmed = true,
                Nickname = "Mr. White",
                ProfilePicture = "/images/boosters/heisenberg.jpg"
            };
            user2.PasswordHash = _passwordHasher.HashPassword(user2, "123456");

            users.Add(user2);

            var user3 = new ApplicationUser
            {
                Id = "54cf5237-4a7c-4050-9570-7cb5cb753aa5",
                UserName = "dva@booster.com",
                NormalizedUserName = "DVA@BOOSTER.COM",
                Email = "dva@booster.com",
                NormalizedEmail = "DVA@BOOSTER.COM",
                EmailConfirmed = true,
                Nickname = "D.VA",
                ProfilePicture = "/images/boosters/dva.jpg"
            };
            user3.PasswordHash = _passwordHasher.HashPassword(user3, "123456");

            users.Add(user3);

            var user4 = new ApplicationUser
            {
                Id = "ca3439dd-d67e-4733-8b72-a497af8b4c64",
                UserName = "johnwick@booster.com",
                NormalizedUserName = "JOHNWICK@BOOSTER.COM",
                Email = "johnwick@booster.com",
                NormalizedEmail = "JOHNWICK@BOOSTER.COM",
                EmailConfirmed = true,
                Nickname = "Mr. Wick",
                ProfilePicture = "/images/boosters/john-wick.jpg"
            };
            user4.PasswordHash = _passwordHasher.HashPassword(user4, "123456");

            users.Add(user4);

            var user5 = new ApplicationUser
            {
                Id = "362456a6-e52a-4065-a619-7af22c96e1e1",
                UserName = "obiwan@author.com",
                NormalizedUserName = "OBIWAN@AUTHOR.COM",
                Email = "obiwan@author.com",
                NormalizedEmail = "OBIWAN@AUTHOR.COM",
                EmailConfirmed = true,
                Nickname = "Master Kenobi",
                ProfilePicture = "/images/authors/obiwan.jpg"
            };
            user5.PasswordHash = _passwordHasher.HashPassword(user5, "123456");

            users.Add(user5);

            var user6 = new ApplicationUser
            {
                Id = "851792db-67bb-4b08-8c03-ac2643a0600a",
                UserName = "quigon@author.com",
                NormalizedUserName = "QUIGON@AUTHOR.COM",
                Email = "quigon@author.com",
                NormalizedEmail = "QUIGON@AUTHOR.COM",
                EmailConfirmed = true,
                Nickname = "Qui-Gon-Jinn",
                ProfilePicture = "/images/authors/quigon.jpg"
            };
            user6.PasswordHash = _passwordHasher.HashPassword(user6, "123456");

            users.Add(user6);

            var user7 = new ApplicationUser
            {
                Id = "2eff52b0-3277-456f-ac78-34553d260ac6",
                UserName = "thebat@client.com",
                NormalizedUserName = "THEBAT@CLIENT.COM",
                Email = "thebat@client.com",
                NormalizedEmail = "THEBAT@CLIENT.COM",
                EmailConfirmed = true,
                Nickname = "Master Wayne",
                ProfilePicture = "/images/clients/thebat.jpg"
            };
            user7.PasswordHash = _passwordHasher.HashPassword(user7, "123456");

            users.Add(user7);

            var user8 = new ApplicationUser
            {
                Id = "1291e6cd-1aac-4f8a-af4d-4980f64aff27",
                UserName = "theone@client.com",
                NormalizedUserName = "THEONE@CLIENT.COM",
                Email = "theone@client.com",
                NormalizedEmail = "THEONE@CLIENT.COM",
                EmailConfirmed = true,
                Nickname = "Mr. Anderson",
                ProfilePicture = "/images/clients/theone.jpg"
            };
            user8.PasswordHash = _passwordHasher.HashPassword(user8, "123456");

            users.Add(user8);

            var user9 = new ApplicationUser
            {
                Id = "97e8127d-abaf-4980-9938-e388453fcbb4",
                UserName = "leon@client.com",
                NormalizedUserName = "LEON@CLIENT.COM",
                Email = "leon@client.com",
                NormalizedEmail = "LEON@CLIENT.COM",
                EmailConfirmed = true,
                Nickname = "Leon S. Kennedy",
                ProfilePicture = "/images/clients/leon.jpg"
            };
            user9.PasswordHash = _passwordHasher.HashPassword(user9, "123456");

            users.Add(user9);

            var user10 = new ApplicationUser
            {
                Id = "871505e9-3338-487d-8496-760de2e1f2c2",
                UserName = "dante@client.com",
                NormalizedUserName = "DANTE@CLIENT.COM",
                Email = "dante@client.com",
                NormalizedEmail = "DANTE@CLIENT.COM",
                EmailConfirmed = true,
                Nickname = "Dante",
                ProfilePicture = "/images/clients/dante.jpeg"
            };
            user10.PasswordHash = _passwordHasher.HashPassword(user10, "123456");

            users.Add(user10);

            var admin = new ApplicationUser
            {
                Id = "698b474f-7790-4ae6-b186-a3ba3405bf99",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                Nickname = "Admin",
                ProfilePicture = "/images/admin.jpg"
            };
            admin.PasswordHash = _passwordHasher.HashPassword(admin, "123456");

            users.Add(admin);

            return users.ToArray();
        }
    }
}
