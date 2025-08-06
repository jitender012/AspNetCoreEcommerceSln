using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Dtos
{
    public class UpdateCategoryDto : ProductCategoryDto
    {
        public int CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
