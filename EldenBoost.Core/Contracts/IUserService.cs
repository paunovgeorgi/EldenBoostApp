using EldenBoost.Core.Models.User;

namespace EldenBoost.Core.Contracts
{
    public interface IUserService
    {
        Task<string> GetUserNicknameAsync(string userId);
        Task<string> GetProfilePictureByUseIdAsync(string userId);
        Task ChangeProfilePictureAsync(string userId, string imgUrl);
        Task<bool> HasOrdersAsync(string userId);
        Task<IEnumerable<UserListViewModel>> AllAsync();
        Task DemoteAsync(string userId);
        Task ReinstateAsync(string userId);
        Task<UserCountDataModel> GetUserCountDataAsync();
    }
}
