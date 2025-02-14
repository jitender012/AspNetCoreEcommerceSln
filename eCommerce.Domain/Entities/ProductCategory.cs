using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryImage { get; set; }

    public int? ParentCategoryId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<ProductCategory> InverseParentCategory { get; set; } = new List<ProductCategory>();

    public virtual ProductCategory? ParentCategory { get; set; }

    public virtual ICollection<ProductCategoryFeature> ProductCategoryFeatures { get; set; } = new List<ProductCategoryFeature>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
