using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO.ProductDTOs
{
    public class FeatureCategoryDTO
    {
        public int? FeatureCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsMandatory { get; set; }

        public int? DisplayOrder { get; set; }

        public string? CreatedBy { get; set; } 
        public int? ProductCategoryId { get; set; }
    }
}
