using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public Guid ProductVariantId { get; set; }

    public int WarehouseId { get; set; }

    public int? StockQuantity { get; set; }

    public int? ReservedQuantity { get; set; }

    public int? ReorderLevel { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
