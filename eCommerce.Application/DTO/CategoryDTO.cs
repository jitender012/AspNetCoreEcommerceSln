using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }

        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; } = null!;
        public string? ParentCategoryImage { get; set; }

        public List<CategoryDTO>? ChildCategoris { get; set; }

        public static CategoryDTO FromCategory(ProductCategory category)
        {
            CategoryDTO categoryDTO = new()
            {
                CategoryId = category.ProductCategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                ParentCategoryId = category.ParentCategoryId,              
                ChildCategoris = GetChildCategories(category.InverseParentCategory)
            };
            return categoryDTO;
        }
        private static List<CategoryDTO> GetChildCategories(ICollection<ProductCategory> categories)
        {
            var categoryDTO_list = categories.Select(x => new CategoryDTO
            {
                CategoryId = x.ProductCategoryId,
                CategoryName = x.CategoryName,
                CategoryImage = x.CategoryImage,
                ParentCategoryId = x.ParentCategoryId,
                ChildCategoris = GetChildCategories(x.InverseParentCategory)
            }).ToList();
            return categoryDTO_list;
        }
        public static List<CategoryDTO> FromCategoryList(List<ProductCategory> categories)
        {
            return categories.Select(c => FromCategory(c)).ToList();
        }
    }


}
