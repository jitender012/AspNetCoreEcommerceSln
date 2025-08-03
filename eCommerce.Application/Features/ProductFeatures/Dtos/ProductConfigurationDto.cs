using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Dtos
{
    public class ProductConfigurationDto
    {
        public int FeatureOptionId { get; set; }

        public Guid ProductVarientId { get; set; }
    }
}
