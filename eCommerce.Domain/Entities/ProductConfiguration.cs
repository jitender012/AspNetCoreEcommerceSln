using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class ProductConfiguration
{
    public int ProductConfigurationId { get; set; }

    public int FeatureOptionId { get; set; }

    public Guid ProductVariantId { get; set; }

    public virtual FeatureOption FeatureOption { get; set; } = null!;

    public virtual ProductVariant ProductVarient { get; set; } = null!;
}
