using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Dtos
{
    public class FeatureCategoryWithFeaturesDto
    {        
        public string FeatureCategoryName { get; set; } = null!; 
        public List<FeaturesDto> ProductFeatures { get; set; } = new();
    }
}
