using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository repository;
        public ApplicationService(IRepository _repository)
        {
            repository = _repository; 
        }
        public async Task<bool> HasAppliedByUserIdAsync(string userId, Expression<Func<Application, bool>> predicate)
        {
            return await repository.AllReadOnly<Application>()
              .Where(predicate)
              .AnyAsync(a => a.UserId == userId);
        }
    }
}
