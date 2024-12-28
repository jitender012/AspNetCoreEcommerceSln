using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class FeatureOption
{
    public int FeatureOptionId { get; set; }

    public int FeatureId { get; set; }

    public string Value { get; set; } = null!;

    public virtual ProductFeature Feature { get; set; } = null!;
}
