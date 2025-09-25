namespace eCommerce.Web.ViewModels.ProductVMs
{
    public class ProductListVM
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Url { get; set; }
        public int VariantCount { get; set; }
    }
}