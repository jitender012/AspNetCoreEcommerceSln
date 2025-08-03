using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Dtos
{
    public class FeatureCategoryDto
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;
        public int? DisplayOrder { get; set; }

        public List<ProductFeatureDto> ProductFeatures { get; set; } = null!;
    }
}
