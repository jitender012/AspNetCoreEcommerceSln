using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Dtos
{
    public class ProductCategoryListDto
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ParentCategoryName { get; set; }       
    }
}
