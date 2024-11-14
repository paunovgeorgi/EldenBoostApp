using EldenBoost.Core.Models.Application;
using EldenBoost.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
    }
}
