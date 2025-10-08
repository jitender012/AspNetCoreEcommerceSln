using eCommerce.Application.DTO;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace eCommerce.Application.Features.ProductCategoryFeatures.Dtos
{
    public class ProductCategoryDetailsDto
    {
        // --- Basic Info ---
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public bool? IsActive { get; set; }                
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public List<FeatureCategoriesWithLinkStatusDto> FeatureCategories { get; set; } = new();
    }
  
}
