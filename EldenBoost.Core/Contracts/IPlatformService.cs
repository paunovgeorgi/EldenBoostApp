using EldenBoost.Core.Models.Platform;

namespace EldenBoost.Core.Contracts
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformFormModel>> GetAllPlatformsAsync();
		Task<bool> PlatformExistsByIdAsync(int id);

	}
}
