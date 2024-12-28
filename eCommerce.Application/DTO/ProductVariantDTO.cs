using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO
{
    public class ProductVariantDTO
    {
        public Guid ProductIvarientId { get; set; }
        public string? VarientName { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }
        public string Sku { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
