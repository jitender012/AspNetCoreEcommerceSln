using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Dtos
{
    public class SellerProductVariantDto : ProductVariantDto
    {
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }
        public List<FeatureCategoryDto> FeatureCategorise { get; set; } = null!;
    }
}
