using eCommerce.Application.DTO.ProductDTOs;

namespace eCommerce.Application.Features.ProductFeatures.Dtos
{
    public class ProductVariantDto
    {
        public Guid ProductIvarientId { get; set; }
        public string? VarientName { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }
        public string Sku { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }

        public IEnumerable<ProductImagesDTO>? ProductImagesDTO { get; set; }
        public IEnumerable<FeatureOptionDto>? FeatureOptions { get; set; }
    }
}
