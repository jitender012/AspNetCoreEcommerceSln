using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class BrandListVM
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProductCount { get; set; }
    }
}
