using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Entities;

public partial class VariationCategory
{
    public int VariationCategoryId { get; set; }

    public int ProductFeaturesId { get; set; }

    public int CategoryId { get; set; }

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual ProductFeature ProductFeatures { get; set; } = null!;
}
