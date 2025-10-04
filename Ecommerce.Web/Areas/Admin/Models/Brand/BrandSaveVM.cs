using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class BrandSaveVM
    {
        public Guid BrandId { get; set; }
        
        public string? BrandName { get; set; }

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
