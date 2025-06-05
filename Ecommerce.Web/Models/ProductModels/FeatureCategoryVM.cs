namespace eCommerce.Web.Models.ProductModels
{
    public class FeatureCategoryVM
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }      

        public string CreatedBy { get; set; } = null!;

        public List<ProductFeaturesVM>? ProductFeatures { get; set; }
    }
}
