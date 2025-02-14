using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductIvariantd { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ProductVariant ProductIvariantdNavigation { get; set; } = null!;
}
