using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Dtos
{
    public class BrandSaveDTO
    {
        public Guid BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public bool? IsActive { get; set; }     
    }
}
