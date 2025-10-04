namespace eCommerce.Web.Areas.Admin.Models.ProductFeature
{
    public class FeatureOptionSaveVM
    {
        public int FeatureOptionId { get; set; }
        public int ProductFeatureId { get; set; }
        public string Value { get; set; } = null!;       
    }
}
