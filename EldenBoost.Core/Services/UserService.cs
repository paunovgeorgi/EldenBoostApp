using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task ChangeProfilePictureAsync(string userId, string imgUrl)
        {
            ApplicationUser? user = await repository.All<ApplicationUser>()
               .Where(a => a.Id == userId)
               .FirstOrDefaultAsync();

            if (user != null)
            {
                user.ProfilePicture = imgUrl;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<string> GetProfilePictureByUseIdAsync(string userId)
        {
            string? profilePicture = await repository.AllReadOnly<ApplicationUser>()
               .Where(u => u.Id == userId)
               .Select(u => u.ProfilePicture)
               .FirstOrDefaultAsync();

            if (profilePicture != null)
            {
                return profilePicture;
            }

            return string.Empty;
        }

        public async Task<string> GetUserNicknameAsync(string userId)
        {
            string? nickname = await repository.AllReadOnly<ApplicationUser>()
               .Where(u => u.Id == userId)
               .Select(u => u.Nickname)
               .FirstOrDefaultAsync();

            if (nickname == null)
            {
                return string.Empty;
            }
            return nickname;
        }
    }
}
