using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductConfigurationFeature.DTOs
{
    public class FeatureOptionSaveDTO
    {
        public int FeatureOptionId { get; set; }
        public int ProductFeatureId { get; set; }
        public string Value { get; set; } = null!;        
    }
}
