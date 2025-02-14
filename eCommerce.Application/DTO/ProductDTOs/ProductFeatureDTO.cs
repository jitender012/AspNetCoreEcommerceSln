namespace eCommerce.Application.DTO.ProductDTOs
{
    public class ProductFeatureDTO
    {
        public int ProductFeaturesId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsManadatory { get; set; }

        public string CreatedBy { get; set; } = null!;
        public int FeatureCategoryId {get; set;} 
        public int? ProductCategoryId {get; set;} 
    }
}
