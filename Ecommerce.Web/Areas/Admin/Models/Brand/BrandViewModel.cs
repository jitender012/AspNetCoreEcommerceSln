using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class BrandViewModel
    {
        public Guid BrandId { get; set; }
        [Required]
        public string BrandName { get; set; } = null!;
        [StringLength(500)]         
        public string? BrandDescription { get; set; }
        public string? BrandImage { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
