namespace EldenBoost.Core.Models.Application
{
    public class ApplicationListViewModel
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public int Availability { get; set; }
        public string Experience { get; set; } = null!;
        public string Platforms { get; set; } = null!;
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
    }

}