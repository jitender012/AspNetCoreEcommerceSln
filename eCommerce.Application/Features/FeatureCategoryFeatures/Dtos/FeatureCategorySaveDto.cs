using eCommerce.Application.Common.Dtos;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Dtos
{
    public class FeatureCategorySaveDto
    {
        public int FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }
    }
}
