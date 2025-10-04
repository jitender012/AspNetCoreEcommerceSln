using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Dtos
{
    public class BrandListDTO
    {
        public Guid BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }        

        public bool? IsActive { get; set; }        

        public DateTime CreatedAt { get; set; }

        public int ProductCount { get; set; }
        
    }
}
