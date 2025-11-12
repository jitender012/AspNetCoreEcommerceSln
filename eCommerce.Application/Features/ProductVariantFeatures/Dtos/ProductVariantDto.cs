using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Dtos
{
    public class ProductVariantDto
    {
        public Guid ProductVariantId { get; set; }

        public string? VarientName { get; set; }        

        public string? ImageUrl { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }        

        public string? Barcode { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Draft;
    }
}
