using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO
{
    public class BrandDTO
    {
        public Guid BrandId { get; set; }
        
        [Required]
        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public Guid? CreatedBy { get; set; }


        public Guid? UpdatedBy { get; set; }



        public Brand ToBrand()
        {
            return new Brand()
            {
                BrandId = BrandId,
                BrandName = BrandName,
                BrandImage = BrandImage,
                BrandDescription = BrandDescription,
                CreatedBy = CreatedBy,            
                UpdatedBy = UpdatedBy
            };
        }
        public static BrandDTO FromBrand(Brand brand)
        {
            return new BrandDTO
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                BrandImage = brand.BrandImage,
                BrandDescription = brand.BrandDescription,
                CreatedBy = brand.CreatedBy,              
                UpdatedBy = brand.UpdatedBy
            };
        }
        public static List<BrandDTO> FromBrandList(IEnumerable<Brand> brands)
        {
            return brands.Select(b => FromBrand(b)).ToList();
        }
    }
}
