using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class VariationCategory
{
    public int VariationCategoryId { get; set; }

    public int ProductFeaturesId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ProductFeature ProductFeatures { get; set; } = null!;
}
