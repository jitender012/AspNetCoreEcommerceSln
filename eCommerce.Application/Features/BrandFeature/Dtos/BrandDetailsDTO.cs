using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Dtos
{
    public class BrandDetailsDTO
    {
        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public bool? IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
