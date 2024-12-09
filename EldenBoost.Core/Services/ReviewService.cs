using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Review;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;
        public ReviewService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateReviewAsync(string content, string userId)
        {
            Review review = new Review
            {
                Content = content,
                UserId = userId,
                ReviewDate = DateTime.UtcNow,
            };

            await repository.AddAsync(review);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewCardViewModel>> GetLatestReviewsAsync()
        {
            return await repository.AllReadOnly<Review>()
                .Include(r => r.User)
                .OrderByDescending(r => r.Id)
                .Take(8)
                .Select(r => new ReviewCardViewModel()
                {
                    Id = r.Id,
                    Content = r.Content,
                    Nickname = r.User!.Nickname ?? "User",
                    ProfilePicture = r.User!.ProfilePicture ?? string.Empty,
                    ReviewDate = r.ReviewDate.ToShortDateString()
                })
                .ToListAsync();
        }
    }
}
