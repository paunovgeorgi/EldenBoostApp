using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class BoosterService : IBoosterService
    {
        private readonly IRepository repository;
        public BoosterService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<bool> BoosterExistsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Booster>().AnyAsync(u => u.UserId == userId);
        }
    }
}
