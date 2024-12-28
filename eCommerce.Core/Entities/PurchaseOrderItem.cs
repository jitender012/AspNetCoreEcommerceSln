using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class PurchaseOrderItem
{
    public Guid PurchaseOrderItemId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public Guid? ProductVariantId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitCost { get; set; }

    public virtual PurchaseOrder? PurchaseOrder { get; set; }
}
