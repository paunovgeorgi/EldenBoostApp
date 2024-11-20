using EldenBoost.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IUserService
    {
        Task<string> GetUserNicknameAsync(string userId);
        Task<string> GetProfilePictureByUseIdAsync(string userId);
        Task ChangeProfilePictureAsync(string userId, string imgUrl);
        Task<bool> HasOrdersAsync(string userId);
        Task<IEnumerable<UserListViewModel>> AllAsync();

    }
}
