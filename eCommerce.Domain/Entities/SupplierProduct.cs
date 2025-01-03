using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class SupplierProduct
{
    public int SupplierProductId { get; set; }

    public int SupplierId { get; set; }

    public Guid ProductVariantId { get; set; }

    public decimal UnitCost { get; set; }

    public bool? IsActive { get; set; }

    public virtual ProductVarient ProductVariant { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
