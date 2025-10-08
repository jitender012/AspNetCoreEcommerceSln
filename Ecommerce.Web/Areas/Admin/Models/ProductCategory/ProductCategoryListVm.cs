namespace eCommerce.Web.Areas.Admin.Models.ProductCategory
{
    public class ProductCategoryListVm
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
