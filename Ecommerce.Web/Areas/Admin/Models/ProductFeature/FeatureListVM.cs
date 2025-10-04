using eCommerce.Domain.Entities;

namespace eCommerce.Web.Areas.Admin.Models.ProductFeature
{
    public class FeatureListVM
    {
        public int ProductFeaturesId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }
        public string? FeatureCategoryName { get; set; }
        public string? MeasurementUnit { get; set; }
    }
}
