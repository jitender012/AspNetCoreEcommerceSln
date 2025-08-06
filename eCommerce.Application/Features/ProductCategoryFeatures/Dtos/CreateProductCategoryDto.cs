using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Dtos
{
    public class CreateProductCategoryDto : ProductCategoryDto
    {
        public int? ParentCategoryId { get; set; }
    }
}
