using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Dtos
{
    public class ProductVariantSaveDTO
    {
        public Guid ProductIvarientId { get; set; }

        public string? VarientName { get; set; }

        public int? Quantity { get; set; }

        public string SKU { get; set; } = null!;

        public decimal Price { get; set; }

        public bool? IsActive { get; set; } = true;

        public List<string> ImageUrls { get; set; } = [];

        public List<ProductFeatureDto> Features { get; set; } = [];
    }
}
