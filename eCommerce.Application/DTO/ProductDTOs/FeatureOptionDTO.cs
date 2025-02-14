using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class FeatureOptionDTO
    {
        public int FeatureOptionId { get; set; }
        public int ProductFeatureId { get; set; }
        public string Value { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
