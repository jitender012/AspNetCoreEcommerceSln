using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class FeatureCategory
{
    public int FeatureCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsMandatory { get; set; }
    public bool IsDeleted { get; set; }

    public int? DisplayOrder { get; set; }

    public string CreatedBy { get; set; } = null!;
            

    /// <summary>
    /// Gets or sets the collection of features associated with the product category.
    /// </summary>
    public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = [];
    
}
