using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Product
{
    public class ProductFeatureViewModel
    {
        public int ProductFeaturesId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public bool? IsManadatory { get; set; }

        public string CreatedBy { get; set; } = null!;
        public int FeatureCategoryId { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
