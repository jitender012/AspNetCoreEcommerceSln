using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ExpectedDelivery { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

    public virtual Supplier? Supplier { get; set; }
}
