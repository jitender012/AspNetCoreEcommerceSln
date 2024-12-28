using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class BrandCreateViewModel
    {
        [Required]
        public string BrandName { get; set; } = null!;
        [StringLength(500)]         
        public string? BrandDescription { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
