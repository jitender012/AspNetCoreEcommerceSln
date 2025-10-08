using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductConfigurationFeature.DTOs
{
    public class FeatureNCategoryIdsDto
    {
        public int CategoryId { get; set; }
        public List<int> FeatureIds { get; set; } = [];
    }
}
