using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WarehouseFeature.Dtos
{
    public class WarehouseListDto
    {
        public string Name { get; set; } = null!;

        public string? Email { get; set; }        

        public string? City { get; set; }
    }
}
