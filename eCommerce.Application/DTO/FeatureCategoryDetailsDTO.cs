using eCommerce.Application.DTO.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO
{
    public class FeatureCategoryDetailsDTO
    {
        public int? FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool IsMandatory { get; set; }

        public int? DisplayOrder { get; set; }

        public string? CreatedBy { get; set; }

        public List<ProductFeatureDTO>? ProductFeatures { get; set; }
        public List<CategoryDTO>? ProductCategories { get; set; }
    }
}
