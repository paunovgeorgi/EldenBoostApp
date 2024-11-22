using EldenBoost.Core.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewCardViewModel>> GetLatestReviewsAsync();
        Task CreateReviewAsync(string content, string userId);
    }
}
