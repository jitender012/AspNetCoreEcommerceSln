using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductVariantId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
