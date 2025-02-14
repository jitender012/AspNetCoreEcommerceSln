namespace eCommerce.Web.Areas.Admin.Models.Category
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;       
        public string? CategoryImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public string? ParentCategoryImage { get; set; }

        public IEnumerable<IFormFile>? ImageFile { get; set; }

    }
    public class CategoryDropDownModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public List<CategoryDropDownModel>? categories { get; set; }
    }
}
