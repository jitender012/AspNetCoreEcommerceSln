namespace eCommerce.Web.Areas.Seller.Models.InventoryModels;

public class InventoryViewModel
{
    public int InventoryId { get; set; }

    public string ProductVariantName { get; set; } = null!;

    public string WarehouseName { get; set; } = null!;

    public int? StockQuantity { get; set; }

    public int? ReservedQuantity { get; set; }
}