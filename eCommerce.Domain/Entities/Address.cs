using eCommerce.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities;

public partial class Address
{ 
    public int Id { get; set; }
    public Guid UserId { get; set; }

    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }

    public string? AddressType { get; set; }
    public string? Street { get; set; }
    public string? Landmark { get; set; }

    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    public bool? IsDefault { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ApplicationUser User { get; set; } = null!;
}
