namespace EldenBoost.Core.Models.Review
{
    public class ReviewCardViewModel
    {
        public int Id { get; set; }

        public string Nickname { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string ReviewDate { get; set; } = null!;
    }
}
