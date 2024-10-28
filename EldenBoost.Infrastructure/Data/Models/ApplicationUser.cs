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
        public string? ProfilePicture { get; set; }

    }
}
