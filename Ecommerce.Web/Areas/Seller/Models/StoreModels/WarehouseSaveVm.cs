using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Seller.Models.StoreModels
{
    public class WarehouseSaveVm
    {
        public Guid WarehouseId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }
    }
}
