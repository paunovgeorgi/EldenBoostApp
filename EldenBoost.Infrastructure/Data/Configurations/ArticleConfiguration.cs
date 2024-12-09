using EldenBoost.Common.Enumerations;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(GenerateArticles());
        }

        private Article[] GenerateArticles()
        {
            ICollection<Article> articles = new List<Article>();

            Article article;

            article = new Article
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

            articles.Add(article);

            article = new Article
            {
                Id = 2,
                Title = "Best Weapons in Elden Ring",
                ArticleType = ArticleType.Guide,
                AuthorId = 1,
                ImageURL = "/images/articles/best-weapons.jpg",
                ReleaseDate = new DateTime(2024, 10, 2),
                Content = "In this article, we dive deep into the best weapons for every stage of Elden Ring. From the devastating power of colossal swords to the agility of twin blades, we cover a range of weapons to suit any playstyle. " +
                          "Each weapon offers a unique gameplay experience, and our guide will help you maximize your potential by selecting the best gear to fit your build and approach."
            };

            articles.Add(article);

            article = new Article
            {
                Id = 3,
                Title = "Elden Ring: Secrets You Might Have Missed",
                ArticleType = ArticleType.News,
                AuthorId = 2,
                ImageURL = "/images/articles/secrets.jpg",
                ReleaseDate = new DateTime(2024, 10, 3),
                Content = "Elden Ring is a game rich with hidden secrets and subtle story elements. In this article, we bring you a curated list of Easter eggs and obscure details that you might have overlooked in your journey through the Lands Between. " +
                          "From unique NPC dialogues to hidden locations and items, discover the hidden lore and secrets that make the world of Elden Ring come alive in unexpected ways."
            };

            articles.Add(article);

            article = new Article
            {
                Id = 4,
                Title = "Elden Ring: Patch Notes Breakdown",
                ArticleType = ArticleType.News,
                AuthorId = 2,
                ImageURL = "/images/articles/patch-notes.jpg",
                ReleaseDate = new DateTime(2024, 10, 4),
                Content = "The latest patch has introduced some significant changes in Elden Ring. This article breaks down the most impactful updates, from balancing tweaks to bug fixes and new features. " +
                          "We'll go over which changes will affect players the most and what new content you can look forward to in this updated version of the game."
            };

            articles.Add(article);

            article = new Article
            {
                Id = 5,
                Title = "How to Beat Malenia, Blade of Miquella",
                ArticleType = ArticleType.Guide,
                AuthorId = 1,
                ImageURL = "/images/articles/malenia.jpg",
                ReleaseDate = new DateTime(2024, 10, 5),
                Content = "Malenia is one of the most challenging encounters in Elden Ring, known for her devastating attacks and regenerating health. This guide provides detailed strategies for overcoming her attacks and defeating her. " +
                          "We'll cover the best equipment, skills, and techniques to maximize your chances against this formidable foe. With careful preparation and the right tactics, you can emerge victorious from one of the hardest fights in the game."
            };

            articles.Add(article);

            article = new Article
            {
                Id = 6,
                Title = "Elden Ring: The Best Builds for Every Playstyle",
                ArticleType = ArticleType.Guide,
                AuthorId = 2,
                ImageURL = "/images/articles/best-builds.jpg",
                ReleaseDate = new DateTime(2024, 10, 6),
                Content = "This article delves into the best character builds for various playstyles, from powerful melee warriors to skilled sorcerers. Whether you're seeking the ultimate PvE powerhouse or a versatile PvP build, we cover all the options " +
                          "to help you create a character that suits your preferred approach. Get ready to take on any challenge in the Lands Between with these optimized builds."
            };

            articles.Add(article);

            return articles.ToArray();
        }
    }
}
