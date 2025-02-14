namespace eCommerce.Web.Areas.Admin.Models.Product
{
    public class FeatureCategoryViewModel
    {
        public int? FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }

        public int? DisplayOrder { get; set; }

        public string? CreatedBy { get; set; } 

        public int? ProductCategoryId { get; set; }
    }
}
