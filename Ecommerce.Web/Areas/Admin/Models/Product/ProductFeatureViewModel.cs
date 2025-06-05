using System.ComponentModel.DataAnnotations;
using eCommerce.Domain.Entities;

namespace eCommerce.Web.Areas.Admin.Models.Product
{
    public class ProductFeatureViewModel
    {
        public int ProductFeatureId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public bool IsManadatory { get; set; }
        public FeatureInputType InputType { get; set; }
        public string? CreatedBy { get; set; } = null!;
        public int FeatureCategoryId { get; set; }
        public string? FeatureCategoryName { get; set; } 
        public int? ProductCategoryId { get; set; }
        public List<FeatureOptionsViewModel>? FeatureOptions { get; set; }
    }
}
