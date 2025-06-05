using eCommerce.Domain.Entities;

namespace eCommerce.Web.ViewModels.ProductFeatureVMs
{
    public class ProductFeatureDetailsVM : ProductFeatureVM
    {
        public string FeatureCategoryName { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        //public List<FeatureOptionVM> FeatureOptions { get; set; } = new();
    }
}
