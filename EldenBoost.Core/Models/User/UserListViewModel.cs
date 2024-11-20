using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Models.User
{
    public class UserListViewModel
    {
        public string UserId { get; set; } = null!;

        public string? ProfilePicture { get; set; }

        public string Nickname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal? MoneySpent { get; set; }

        public string Position { get; set; } = "Client";

        public decimal? MoneyMade { get; set; }

        public bool IsBooster { get; set; }

        public bool IsAuthor { get; set; }

        public bool IsDemoted { get; set; }
    }
}
