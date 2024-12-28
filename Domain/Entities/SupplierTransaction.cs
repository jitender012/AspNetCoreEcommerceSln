using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure;

public partial class SupplierTransaction
{
    public long TransactionId { get; set; }

    public int? SupplierId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Remarks { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
