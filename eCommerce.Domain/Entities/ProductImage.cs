using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public string? ImageUrl { get; set; }

    public Guid ProductVariantId { get; set; }

    public bool IsPrimary { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public int? Order { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
