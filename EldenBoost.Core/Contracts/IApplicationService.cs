using EldenBoost.Core.Models.Application;
using EldenBoost.Infrastructure.Data.Models;
using System.Linq.Expressions;

namespace EldenBoost.Core.Contracts
{
    public interface IApplicationService
    {
        Task<bool> HasAppliedByUserIdAsync(string userId, Expression<Func<Application, bool>> predicate);
        Task CreateBoosterApplicationAsync(string userId, ApplicationFormModel model);
        Task CreateAuthorApplicationAsync(string userId, ApplicationFormModel model);
        Task<IEnumerable<ApplicationListViewModel>> AllAsync(Expression<Func<Application, bool>> predicate);
        Task ApproveBoosterAsync(int applicationId);
        Task ApproveAuthorAsync(int applicationId);
        Task RejectAsync(int applicationId);
        Task<ApplicationCountDataModel> GetApplicationCountDataAsync();
    }
}
