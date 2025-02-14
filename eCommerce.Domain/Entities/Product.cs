using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public Guid? BrandId { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    //public virtual Brand ProductNavigation { get; set; } = null!;
    public virtual Brand Brand { get; set; } = null!;


    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual AspNetUser? UpdatedByNavigation { get; set; }
}
