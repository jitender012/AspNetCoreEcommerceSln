namespace eCommerce.Application.Features.FeatureCategoryFeatures.Dtos
{
    public class FeatureCategoriesWithLinkStatusDto
    {
        public int FeatureCategoryId { get; set; }
        public string FeatureCategoryName { get; set; } = null!;
        public List<FeatureDto> Features { get; set; } = new();
    }
    public class FeatureDto
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; } = null!;
        public string? UnitSymbol { get; set; }
        public bool IsLinked { get; set; }
    }
}
