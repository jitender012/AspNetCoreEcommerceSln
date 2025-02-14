using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class FeatureCategory
{
    public int FeatureCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsMandatory { get; set; }

    public int? DisplayOrder { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual ICollection<ProductCategoryFeature> ProductCategoryFeatures { get; set; } = new List<ProductCategoryFeature>();
    
}
