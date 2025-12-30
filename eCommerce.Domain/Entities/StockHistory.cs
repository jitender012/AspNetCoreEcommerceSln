using eCommerce.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public class StockHistory
    {
        public int StockHistoryId { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid SellerId { get; set; }

        public int PreviousQty { get; set; }
        public int ChangedQty { get; set; }
        public int NewQty { get; set; }

        public string ActionType { get; set; } = null!;  // Increase, Decrease, ManualSet
        public string? ActionReason { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ProductVariant ProductVariant { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;        
    }
}
