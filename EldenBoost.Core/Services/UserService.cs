using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.User;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using EldenBoost.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IBoosterService boosterService;
        private readonly IAuthorService authorService;
        private readonly IOrderService orderService;
        public UserService(IRepository _repository,
            IBoosterService _boosterService,
            IAuthorService _authorService,
            IOrderService _orderService)
        {
            repository = _repository;
            boosterService = _boosterService;
            authorService = _authorService;
            orderService = _orderService;
        }

        public async Task<IEnumerable<UserListViewModel>> AllAsync()
        {
            var users = await repository.AllReadOnly<ApplicationUser>()
             .Select(u => new UserListViewModel()
             {
                 UserId = u.Id,
                 ProfilePicture = u.ProfilePicture,
                 Nickname = u.Nickname,
                 Email = u.Email!
             })
             .ToListAsync();

            foreach (var user in users)
            {

                Booster? booster = await boosterService.GetBoosterByUserIdAsync(user.UserId);
                

                if (booster != null)
                {
					bool isActiveBooster = await boosterService.IsActiveAsync(user.UserId);
                    if (!isActiveBooster)
                    {
                        user.IsDemoted = true;
                    }
                    user.Position = "Booster";
                    user.IsBooster = true;
                    user.MoneyMade = booster.TotalEarned;
                }
                else if(await authorService.ExistsByUserIdAsync(user.UserId))
                {
					bool isActiveAuthor = await authorService.IsActiveAsync(user.UserId);
					if (!isActiveAuthor)
					{
						user.IsDemoted = true;
					}
					user.Position = "Author";
                    user.IsAuthor = true;
                }             
                else
                {
                    user.MoneySpent = await orderService.TotalPaidByClientIdAsync(user.UserId);
                }
            }

            return users;
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

        public async Task DemoteAsync(string userId)
        {
            Booster? booster = await repository.All<Booster>()
           .FirstOrDefaultAsync(b => b.UserId == userId);

            Author? author = await repository.All<Author>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (booster != null)
            {
                booster.IsDemoted = true;
            }
            if (author != null)
            {
                author.IsDemoted = true;
            }

            await repository.SaveChangesAsync();
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

        public async Task<bool> HasOrdersAsync(string userId)
        {
            return await repository.AllReadOnly<Order>()
                .AnyAsync(o => o.ClientId == userId);
        }

        public async Task PromoteAsync(string userId)
        {
            Booster? booster = await repository.All<Booster>()
          .FirstOrDefaultAsync(b => b.UserId == userId);

            Author? author = await repository.All<Author>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (booster != null)
            {
                booster.IsDemoted = false;
            }
            if (author != null)
            {
                author.IsDemoted = false;
            }

            await repository.SaveChangesAsync();
        }
    }
}
