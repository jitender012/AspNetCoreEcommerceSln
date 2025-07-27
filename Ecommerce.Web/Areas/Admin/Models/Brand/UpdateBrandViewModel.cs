using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class UpdateBrandViewModel : BrandViewModel
    {
        [Required]
        public Guid BrandId { get; set; }
        public string? BrandImage {  get; set; }
        public Guid? UpdatedBy { get; set; }

    }
}
