using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductImageFeatures.Dtos
{
    public class ProductImageDto
    {
        public string? ImageUrl { get; set; }

        public Guid ProductVariantId { get; set; }

        public bool IsPrimary { get; set; }       

        public int? Order { get; set; }
    }
}
