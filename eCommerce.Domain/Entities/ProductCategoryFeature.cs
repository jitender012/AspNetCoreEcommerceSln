using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class ProductCategoryFeature
{
    public int ProductCategoryFeatureId { get; set; }

    public int ProductCategoryId { get; set; }

    public int FeatureCategoryId { get; set; }

    public int? ProductFeaturesId { get; set; }

    public virtual FeatureCategory FeatureCategory { get; set; } = null!;

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ProductFeature? ProductFeatures { get; set; }
}
