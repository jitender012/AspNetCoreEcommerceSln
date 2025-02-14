namespace eCommerce.Web.Models.ProductModels
{
    public class FeatureOptionViewModel
    {
        public int FeatureOptionId { get; set; }
        public int ProductFeatureId { get; set; }
        public string Value { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
