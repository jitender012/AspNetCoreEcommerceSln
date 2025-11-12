using eCommerce.Application.Features.ProductVariantFeatures.Dtos;

namespace eCommerce.Application.Features.ProductFeatures.Dtos
{
    public class ProductSaveDTO
    {                
        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }        

        public Guid BrandId { get; set; }

        public int CategoryId { get; set; }    

        public ProductVariantSaveDTO ProductVariant { get; set; } = new();
        
        public List<FeatureCategoryWithFeaturesDto>? FeatureCategory { get; set; }
    }
}
