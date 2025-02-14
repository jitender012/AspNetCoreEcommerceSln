using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public Guid ProductVariantId { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AspNetUser Customer { get; set; } = null!;

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
