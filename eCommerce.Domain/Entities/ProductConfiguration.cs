using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class ProductConfiguration
{
    public int FeatureOptionId { get; set; }

    public Guid ProductVarientId { get; set; }

    public virtual ProductFeature FeatureOption { get; set; } = null!;

    public virtual ProductVarient ProductVarient { get; set; } = null!;
}
