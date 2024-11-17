using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public int PlatformId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool? HasStream { get; set; }

        public bool? IsExpress { get; set; }

        public int? OptionId { get; set; }

        public int SliderValue { get; set; }
        public string? Information { get; set; }
    }
}
