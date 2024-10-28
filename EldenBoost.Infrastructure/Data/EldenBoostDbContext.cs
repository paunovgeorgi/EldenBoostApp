using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Data
{
    public class EldenBoostDbContext : IdentityDbContext<ApplicationUser>
    {
        public EldenBoostDbContext(DbContextOptions<EldenBoostDbContext> options)
            : base(options)
        {
        }
    }
}
