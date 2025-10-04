using eCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Models.FeatureCategory
{
    public class FeatureCategorySaveVm
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }
    }
}
