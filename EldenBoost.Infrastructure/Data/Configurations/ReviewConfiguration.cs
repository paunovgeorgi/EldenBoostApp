using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(GenerateReviews());
        }

        private Review[] GenerateReviews()
        {
            ICollection<Review> reviews = new HashSet<Review>();

            Review review;

            review = new Review()
            {
                Id = 1,
                Content = "Amazing service! Got my Runes fast, and the booster was super friendly. Helped me clear some tough bosses I’ve been stuck on for weeks. Totally worth it! Will definitely use this service again. 10/10!",
                UserId = "2eff52b0-3277-456f-ac78-34553d260ac6",
                ReviewDate = new DateTime(2024, 9, 3)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 2,
                Content = "Had an awesome experience! The boosting team was professional and kept me updated the whole time. Got all the items I wanted in no time. Highly recommended for anyone looking to enjoy Elden Ring without the grind",
                UserId = "1291e6cd-1aac-4f8a-af4d-4980f64aff27",
                ReviewDate = new DateTime(2024, 10, 2)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 3,
                Content = "I was skeptical at first, but the service was flawless. The booster got me through some difficult areas and my gear is maxed out now. Fast, safe, and affordable. If you need help, this is the place to go!",
                UserId = "97e8127d-abaf-4980-9938-e388453fcbb4",
                ReviewDate = new DateTime(2024, 10, 8)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 4,
                Content = "I’ve been struggling with some of the harder bosses in Elden Ring, and this boost service saved me a ton of time. Professional, reliable, and quick delivery. If you’re stuck in the game, I highly recommend it!",
                UserId = "871505e9-3338-487d-8496-760de2e1f2c2",
                ReviewDate = new DateTime(2024, 11, 4)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 5,
                Content = "The best boosting service out there! I needed some runes and boss kills, and they delivered everything faster than expected. The process was smooth, and my account was in safe hands. Will definitely come back!",
                UserId = "2eff52b0-3277-456f-ac78-34553d260ac6",
                ReviewDate = new DateTime(2024, 11, 7)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 6,
                Content = "Fantastic job by the team! I got exactly what I asked for in a short time. The Play with a Pro option was especially fun – I learned so much. Super safe and legit, I couldn't have asked for a better experience.",
                UserId = "1291e6cd-1aac-4f8a-af4d-4980f64aff27",
                ReviewDate = new DateTime(2024, 11, 11)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 7,
                Content = "Ordered a boosting service to get past some difficult bosses. The service was quick and efficient, and the booster communicated throughout. Completely satisfied with the results and highly recommend this service!",
                UserId = "97e8127d-abaf-4980-9938-e388453fcbb4",
                ReviewDate = new DateTime(2024, 11, 24)
            };

            reviews.Add(review);

            review = new Review()
            {
                Id = 8,
                Content = "A flawless service! Elden Ring is tough, but these guys made it easy. My character is leveled up, and I have all the gear I wanted. Great support, timely delivery, and no hassles. Can’t recommend them enough!",
                UserId = "871505e9-3338-487d-8496-760de2e1f2c2",
                ReviewDate = new DateTime(2024, 11, 29)
            };

            reviews.Add(review);

            return reviews.ToArray();
        }
    }
}
