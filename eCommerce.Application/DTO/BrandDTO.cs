using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO
{
    public class CreateBrandDTO
    {
        public Guid BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public Guid? CreatedBy { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public System.DateTime? UpdatedAt { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }


        public Brand ToBrand()
        {
            return new Brand()
            {
                BrandId = BrandId,
                BrandName = BrandName,
                BrandImage = BrandImage,
                BrandDescription = BrandDescription,
                CreatedBy = CreatedBy,
                IsDeleted = IsDeleted,
                UpdatedAt =UpdatedAt,
                CreatedAt = CreatedAt,
                IsActive = IsActive,
                UpdatedBy = UpdatedBy
            };
        }
        public static CreateBrandDTO FromBrand(Brand brand)
        {
            return new CreateBrandDTO
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                BrandImage = brand.BrandImage,
                BrandDescription = brand.BrandDescription,
                CreatedBy = brand.CreatedBy,
                IsDeleted = brand.IsDeleted,
                UpdatedAt =brand.UpdatedAt,
                CreatedAt = brand.CreatedAt,
                IsActive = brand.IsActive,
                UpdatedBy = brand.UpdatedBy
            };
        }
        public static List<CreateBrandDTO> FromBrandList(IEnumerable<Brand> brands)
        {
            return brands.Select(b => FromBrand(b)).ToList();
        }
    }
}
