using EldenBoost.Common.Enumerations;
using EldenBoost.Data;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Tests.Mocks;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System;

namespace EldenBoost.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected EldenBoostDbContext data;

        [OneTimeSetUp]

        public void SetUpBase()
        {
            data = DatabaseMock.Instance;
            SeedDataBase();
        }

        public ApplicationUser User { get; private set; }
        public ApplicationUser User2 { get; private set; }
        public ApplicationUser User3 { get; private set; }
        public Booster Booster { get; private set; }
        public Author Author { get; private set; }

        public Service Service { get; private set; }
        public Service Service2 { get; set; }
        public Service ServiceWithOptions { get; set; }
        public ServiceOption ServiceOption1 { get; set; }
        public ServiceOption ServiceOption2 { get; set; }
        public Platform Platform { get; set; }

        public CartItem CartItem { get; set; }
        public Order Order { get; set; }
        public Article Article { get; set; }

        public Review Review { get; set; }

        public Application AuthorApplication { get; set; }
        public Application BoosterApplication { get; set; }

        public void SeedDataBase()
        {
            User = new ApplicationUser()
            {
                Id = "ClientUserId",
                Email = "client@client.com",
                Nickname = "The Client",
                ProfilePicture = "ClientPicture"
            };

            data.Users.Add(User);

            User2 = new ApplicationUser()
            {
                Id = "BoosterUserId",
                Email = "booster@booster.com",
                Nickname = "The Booster",
                ProfilePicture = "BoosterPicture"
            };

            data.Users.Add(User2);

            User3 = new ApplicationUser()
            {
                Id = "AuthorUserId",
                Email = "author@author.com",
                Nickname = "The Author",
                ProfilePicture = "AuthorPicture"
            };

            data.Users.Add(User3);

            Booster = new Booster()
            {
                Id = 1,
                Country = "Japan",
                UserId = "BoosterUserId"
            };

            data.Boosters.Add(Booster);

            Author = new Author()
            {
                Id = 1,
                Country = "Canada",
                UserId = "AuthorUserId"
            };

            data.Authors.Add(Author);

            Service = new Service()
            {
                Id = 1,
                Title = "Game Completion",
                Description = "You will get a 100% game completion and all the items we aquire along the way!",
                Price = 300.00M,
                ImageURL = "/images/service/gameCompletion.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 0,
                ServiceType = ServiceType.Basic
            };

            data.Services.Add(Service);

            Service2 = new Service()
            {
                Id = 3,
                Title = "Leveling",
                Description = "Select the desired amount of levels you'd like and we'll levep-up your character in no time!",
                Price = 2.50M,
                ImageURL = "/images/service/leveling.png",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 100,
                ServiceType = ServiceType.Slider,
            };
            data.Services.Add(Service2);       

            ServiceOption1 = new ServiceOption()
            {
                Id = 1,
                Name = "Margit the Fell Omen",
                Price = 10.00M,
                ServiceId = 4
            };

            data.ServiceOptions.Add(ServiceOption1);

            ServiceOption2 = new ServiceOption()
            {
                Id = 2,
                Name = "Godrick the Grafted",
                Price = 12.00M,
                ServiceId = 4
            };

            data.ServiceOptions.Add(ServiceOption2);


            ServiceWithOptions = new Service()
            {
                Id = 4,
                Title = "Service with options",
                Description = "This is a service with options",
                Price = 2.50M,
                ImageURL = "ServiceWithOptionPic",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 0,
                ServiceType = ServiceType.Option,
                Options = new List<ServiceOption>() { ServiceOption1, ServiceOption2 }

            };
            data.Services.Add(ServiceWithOptions);


            Platform = new Platform()
            {
                Id = 1,
                Name = "PC"
            };

            data.Add(Platform);

            CartItem = new CartItem()
            {
                CartId = 1,
                ServiceId = Service.Id,
                PlatformId = Platform.Id,
                Price = Service.Price,
                HasStream = true,
                IsExpress = false,
                OptionId = null,
                SliderValue = 10,
                Information = "Selected amount: 10"
            };

            data.CartItems.Add(CartItem);

            Order = new Order()
            {
                Status = "Pending",
                ServiceId = CartItem.ServiceId,
                ClientId = User.Id,
                PlatformId = CartItem.PlatformId,
                TimeOfPurchase = DateTime.Now,
                BoosterPay = CartItem.Price / 2,
                Price = CartItem.Price,
                HasStream = CartItem.HasStream ?? false,
                IsExpress = CartItem.IsExpress ?? false,
                Information = CartItem.Information
            };

            data.Orders.Add(Order);

            Article = new Article
            {
                Id = 1,
                Title = "Toughest Bosses in Elden Ring",
                ArticleType = ArticleType.News,
                AuthorId = 1,
                ImageURL = "/images/articles/toughest-bosses.jpg",
                ReleaseDate = new DateTime(2024, 10, 1),
                Content = "Elden Ring offers players a vast array of formidable foes, each more challenging than the last. In this article, we explore the toughest bosses that push players to their limits. Whether you're facing the relentless strikes of Malenia or the monstrous attacks of Radahn, " +
                         "we’ll dive into the bosses that have left a lasting impact on the community. Prepare to relive some of the most intense and rewarding encounters Elden Ring has to offer."
            };

            data.Articles.Add(Article);


            Review = new Review()
            {
                Id = 1,
                Content = "Amazing service! Got my Runes fast, and the booster was super friendly. Helped me clear some tough bosses I’ve been stuck on for weeks. Totally worth it! Will definitely use this service again. 10/10!",
                UserId = "CLientUserId",
                ReviewDate = new DateTime(2024, 9, 3)
            };
            data.Reviews.Add(Review);

            AuthorApplication = new Application()
            {
                Id = 1,
                UserId = "UserApplyingForAuthor",
                Country = "Atlantis",
                Experience = "Plenty of experience.",
                ApplicationType = ApplicationType.Author,
                Availability = 4
            };
            data.Applications.Add(AuthorApplication);

            BoosterApplication = new Application()
            {
                Id = 2,
                UserId = "UserApplyingForBooster",
                Country = "Nebula",
                Experience = "Boosting for many years, top ranked player.",
                ApplicationType = ApplicationType.Booster,
                Availability = 9
            };
            BoosterApplication.Platforms.Add(Platform);
            data.Applications.Add(BoosterApplication);

            data.SaveChanges();
        }


        [OneTimeTearDown]

        public void TearDownBase() => data.Dispose();

    }
}
