using EldenBoost.Common.Enumerations;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(GenerateServices());
        }

        private Service[] GenerateServices()
        {
            ICollection<Service> services = new HashSet<Service>();
       
            services.Add(new Service
            {
                Id = 1,
                Title = "Game Completion",
                Description = "You will get a 100% game completion and all the items we aquire along the way!",
                Price = 300.00M,
                ImageURL = "images/service/gameCompletion.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 0,
                ServiceType = ServiceType.Basic,
            });

            services.Add(new Service
            {
                Id = 2,
                Title = "Boss Kills",
                Description = "Select the boss you'd like us to defeat and our boosters will get the job done!",
                Price = 10.00M,
                ImageURL = "images/service/bossKills.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 0,
                ServiceType = ServiceType.Option,
            });

            services.Add(new Service
            {
                Id = 3,
                Title = "Leveling",
                Description = "Select the desired amount of levels you'd like and we'll levep-up your character in no time!",
                Price = 2.50M,
                ImageURL = "images/service/leveling.png",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 100,
                ServiceType = ServiceType.Slider,
            });

            services.Add(new Service
            {
                Id = 4,
                Title = "Coaching",
                Description = "A professional Elden Ring player will giude you and coach you along the way of your journey. Simply select the amount of hours you'd like to purchase.",
                Price = 10.00M,
                ImageURL = "images/service/coaching.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 20,
                ServiceType = ServiceType.Slider,
            });

            services.Add(new Service
            {
                Id = 5,
                Title = "Talismans",
                Description = "We'll get you the selected amount of talismans!",
                Price = 1.00M,
                ImageURL = "images/service/talismans.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 100,
                ServiceType = ServiceType.Slider,
            });

            services.Add(new Service
            {
                Id = 6,
                Title = "Shadow of the Erdtree",
                Description = "We will complete the Shadow of the Erdtree DLC for you!",
                Price = 150.00M,
                ImageURL = "images/service/shadow.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 0,
                ServiceType = ServiceType.Basic,
            });

            services.Add(new Service
            {
                Id = 7,
                Title = "Runes",
                Description = "We'll get you the selected amount of runes!",
                Price = 1.50M,
                ImageURL = "images/service/runes.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 100,
                ServiceType = ServiceType.Slider,
            });

            services.Add(new Service
            {
                Id = 8,
                Title = "Dungeon Runs",
                Description = "We will complete the selected dungeon for you!",
                Price = 39.00M,
                ImageURL = "images/service/dungeons.jpg",
                IsActive = true,
                PurchaseCount = 0,
                MaxAmount = 0,
                ServiceType = ServiceType.Option,
            });


            return services.ToArray();
        }
    }
}
