namespace eCommerce.Web.Models.ProductModels
{
    public class ProductFeaturesVM
    {
        public int ProductFeaturesId { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsManadatory { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}
