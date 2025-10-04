namespace eCommerce.Web.Areas.Admin.Models.FeatureCategory
{
    public class FeatureCategoryListVm
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }

        public int? ProductCategoryName { get; set; }
    }
}
