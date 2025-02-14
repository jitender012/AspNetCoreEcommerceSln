using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class ProductDiscount
{
    public int DiscountId { get; set; }

    public Guid ProductVariantId { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? FlatDiscount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
