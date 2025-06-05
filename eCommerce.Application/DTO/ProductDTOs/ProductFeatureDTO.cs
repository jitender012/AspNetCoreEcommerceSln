using eCommerce.Domain.Entities;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class ProductFeatureDTO
    {
        public int ProductFeatureId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsManadatory { get; set; }
        public string CreatedBy { get; set; } = null!;
        public int FeatureCategoryId {get; set;} 
        public string? FeatureCategoryName { get; set; } 
        public int? ProductCategoryId {get; set;}

        public FeatureInputType InputType { get; set; }
        public List<FeatureOptionDTO>? FeatureOptions { get; set; }
    }
}
