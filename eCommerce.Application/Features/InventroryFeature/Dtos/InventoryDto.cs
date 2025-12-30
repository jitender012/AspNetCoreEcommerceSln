using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.InventroryFeature.Dtos
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }

        public string ProductVariantName { get; set; } = null!;
       
        public string WarehouseName { get; set; } = null!;

        public int? StockQuantity { get; set; }

        public int? ReservedQuantity { get; set; }

    }
}
