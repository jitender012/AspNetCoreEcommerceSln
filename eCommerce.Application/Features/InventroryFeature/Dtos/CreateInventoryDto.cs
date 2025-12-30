using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.InventroryFeature.Dtos
{
    public class CreateInventoryDto
    {
        public int InventoryId { get; set; }

        public Guid ProductVariantId { get; set; }

        public Guid WarehouseId { get; set; }

        public int? StockQuantity { get; set; }

        public int? ReservedQuantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
