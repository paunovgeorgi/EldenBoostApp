namespace EldenBoost.Infrastructure.Data.Models
{
    public class BoosterPlatform
    {
        public int BoosterId { get; set; }
        public Booster Booster { get; set; } = null!;

        public int PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;
    }
}
