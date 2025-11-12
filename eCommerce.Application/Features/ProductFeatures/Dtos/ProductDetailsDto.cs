using eCommerce.Application.Features.ProductVariantFeatures.Dtos;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Dtos
{
    public class ProductDetailsDto 
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Url { get; set; }

        public string CategoryName { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public List<ProductVariantDto> ProductVariants { get; set; } = [];
    }
}
