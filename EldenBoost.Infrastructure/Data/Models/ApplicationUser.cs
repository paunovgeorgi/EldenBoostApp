using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.ApplicationUserValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Comment("Nickname for application user")]
        [Required]
        [MaxLength(NicknameMaxLength)]
        public string Nickname { get; set; } = null!;

        [Comment("Profile picture application user.")]
        [MaxLength(ProfilePictureMaxLength)]
        public string? ProfilePicture { get; set; } = "/images/default-avatar.png";

        [Comment("Collection of Reviews for the User")]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();

        public virtual ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();

        public Cart? Cart { get; set; }
    }
}
