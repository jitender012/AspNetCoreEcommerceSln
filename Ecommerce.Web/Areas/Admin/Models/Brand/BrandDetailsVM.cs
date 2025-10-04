namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class BrandDetailsVM
    {
        public string BrandName { get; set; } = null!;

        public string? BrandImage { get; set; }

        public string? BrandDescription { get; set; }

        public bool? IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
