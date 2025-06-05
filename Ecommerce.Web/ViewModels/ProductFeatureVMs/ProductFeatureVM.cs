using eCommerce.Domain.Entities;

namespace eCommerce.Web.ViewModels.ProductFeatureVMs
{
    public class ProductFeatureVM
    {
        public int ProductFeatureId { get; set; }
        public string Name { get; set; } = null!;
        public int? FeatureCategoryId { get; set; }
        public bool? IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }
    }
}
