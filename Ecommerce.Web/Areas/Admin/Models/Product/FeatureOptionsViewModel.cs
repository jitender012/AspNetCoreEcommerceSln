namespace eCommerce.Web.Areas.Admin.Models.Product
{
    public class FeatureOptionsViewModel
    {
        public int FeatureOptionId { get; set; }

        public int ProductFeatureId { get; set; }

        public string Value { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
