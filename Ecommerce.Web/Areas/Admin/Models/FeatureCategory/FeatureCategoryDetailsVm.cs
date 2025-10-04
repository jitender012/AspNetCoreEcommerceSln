using eCommerce.Application.Common.Dtos;
using eCommerce.Application.DTO;

namespace eCommerce.Web.Areas.Admin.Models.FeatureCategory
{
    public class FeatureCategoryDetailsVm
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }

        public int? ProductCategoryName { get; set; }

        public List<IdNameDto<int>>? ProductFeatures { get; set; }
        public List<IdNameDto<int>>? ProductCategories { get; set; }
    }
}
