using EldenBoost.Core.Models.Review;

namespace EldenBoost.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewCardViewModel>> GetLatestReviewsAsync();
        Task CreateReviewAsync(string content, string userId);
    }
}
