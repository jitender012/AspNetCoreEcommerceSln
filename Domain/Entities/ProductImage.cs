using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public string? ImageUrl { get; set; }

    public Guid ProductVariantId { get; set; }

    public bool IsPrimary { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ProductVarient ProductVariant { get; set; } = null!;
}
