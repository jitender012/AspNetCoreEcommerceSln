using eCommerce.Domain.IdentityEntities;
using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Cart
{
    public Guid CartId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ApplicationUser CartNavigation { get; set; } = null!;
}
