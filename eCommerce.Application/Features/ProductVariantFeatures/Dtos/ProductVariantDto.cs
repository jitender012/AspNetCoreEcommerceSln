using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Dtos
{
    public class ProductVariantDto
    {
        public Guid ProductIvarientId { get; set; }
        public string? VarientName { get; set; }
        public Guid ProductId { get; set; }
        public string Sku { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
