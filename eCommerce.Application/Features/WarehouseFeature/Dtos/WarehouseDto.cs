using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WarehouseFeature.Dtos
{
    public class WarehouseDto
    {
        public Guid WarehouseId { get; set; }
        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }
    }
}
