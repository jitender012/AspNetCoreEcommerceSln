using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? CategoryImage { get; set; }

        public int? ParentCategoryId { get; set; }

        public static CategoryDTO FromCategory(Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                ParentCategoryId = category.ParentCategoryId
            };
            return categoryDTO;
        }
        public static List<CategoryDTO> FromCategoryList(List<Category> categories)
        {
            return categories.Select(c => FromCategory(c)).ToList();
        }
    }


}
