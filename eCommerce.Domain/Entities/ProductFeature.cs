using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public enum FeatureInputType
{
    Dropdown = 0,
    Textbox = 1,
    Number = 2,
    Boolean = 3
}

public partial class ProductFeature
{
    public int ProductFeaturesId { get; set; }
    public int? FeatureCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsManadatory { get; set; }
    public FeatureInputType InputType { get; set; }
    public string CreatedBy { get; set; } = null!;

    public virtual FeatureCategory FeatureCategory { get; set; } = null!;

    public virtual ICollection<FeatureOption> FeatureOptions { get; set; } = [];

}
