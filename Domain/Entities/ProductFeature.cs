using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class ProductFeature
{
    public int ProductFeaturesId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FeatureOption> FeatureOptions { get; set; } = new List<FeatureOption>();

    public virtual ICollection<VariationCategory> VariationCategories { get; set; } = new List<VariationCategory>();
}
