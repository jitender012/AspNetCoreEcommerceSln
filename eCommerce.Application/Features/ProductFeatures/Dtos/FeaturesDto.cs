namespace eCommerce.Application.Features.ProductFeatures.Dtos 
{ 
    public class FeaturesDto
    {
        public int ProductFeaturesId { get; set; }        
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
