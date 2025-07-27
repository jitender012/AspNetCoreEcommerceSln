using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Dtos
{
    public class BrandDto
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? BrandDescription { get; set; }
        public string? BrandImage { get; set; }
    }
}
