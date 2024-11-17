using System.ComponentModel.DataAnnotations.Schema;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; } = null!;
        public ApplicationUser Client { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}