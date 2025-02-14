using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class FeatureOption
{
    public int FeatureOptionId { get; set; }

    public int ProductFeatureId { get; set; }

    public string Value { get; set; } = null!;
    public Guid? CreatedBy { get; set; } = null!;

    public virtual ICollection<ProductConfiguration> ProductConfigurations { get; set; } = new List<ProductConfiguration>();

    public virtual ProductFeature ProductFeature { get; set; } = null!;
}
