using eCommerce.Application.DTO;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Dtos
{
    public class UpdateBrandDto
    {
        public Guid BrandId { get; set; }
        
        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public Guid UpdatedBy { get; set; }        

        public Brand ToBrand()
        {
            return new Brand()
            {
                BrandId = BrandId,
                BrandName = BrandName,
                BrandImage = BrandImage,
                BrandDescription = BrandDescription,
                UpdatedAt = DateTime.Now,
                UpdatedBy = UpdatedBy
            };
        }
              
    }
}
