using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Application.Features.ProductImageFeatures.Dtos;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Application.Features.ProductFeatures.Dtos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }    

        public ProductVariantDto? ProductVariant { get; set; }

        public IEnumerable<ProductImageDto>? ProuctImages { get; set; }

        public IEnumerable<ProductFeaturesDto>? ProductFeatures { get; set; }
    }
}
