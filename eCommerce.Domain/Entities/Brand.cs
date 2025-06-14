﻿using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Brand
{
    public Guid BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public string? BrandImage { get; set; }

    public string? BrandDescription { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
