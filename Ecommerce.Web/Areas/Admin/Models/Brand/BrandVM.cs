namespace eCommerce.Web.Areas.Admin.Models.Brand
{
    public class BrandVM
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? BrandImage { get; set; }
        public string? BrandDescription { get; set; }

        // Audit Info
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Status
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        // Extra Info
        public int TotalProducts { get; set; }
        
        public List<string>? ProductNames { get; set; }
    }
}
