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
    public int? MeasurementUnitId { get; set; }
    public string Name { get; set; } = null!;
    public bool? IsManadatory { get; set; }
    public FeatureInputType InputType { get; set; }
    public string CreatedBy { get; set; } = null!;

    public virtual MeasurementUnit? MeasurementUnit { get; set; }
    public virtual FeatureCategory? FeatureCategory { get; set; }
    public virtual ICollection<FeatureOption> FeatureOptions { get; set; } = [];
    public virtual ICollection<ProductCategoryProductFeature> ProductCategoryProductFeatures { get; set; } = new List<ProductCategoryProductFeature>();
}
