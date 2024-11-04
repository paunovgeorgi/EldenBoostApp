using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository repository;
        public AuthorService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Author>()
                .AnyAsync(a => a.UserId == userId);
        }
    }
}
