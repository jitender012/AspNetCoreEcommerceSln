using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class Address
{
    public int AddressId { get; set; }

    public Guid UserId { get; set; }

    public string? AddressType { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public bool? IsDefault { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual AspNetUser User { get; set; } = null!;
}
