using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Common
{
    public interface IInventroyRepository
    {
        // Get all inventories for a specific product variant
        Task<IEnumerable<Inventory>> GetByProductVariantIdAsync(Guid productVariantId);

        // Get inventory for a specific product variant in a specific warehouse
        Task<Inventory?> GetByVariantAndWarehouseAsync(Guid productVariantId, Guid warehouseId);

        // Get warehouse with available stock (for order fulfillment logic)
        Task<Inventory?> GetWarehouseWithAvailableStockAsync(Guid productVariantId, int requiredQty);

        // total available quantity across all warehouses
        Task<int> GetTotalStockAsync(Guid productVariantId);

        // Reduce stock in a warehouse
        Task<bool> ReduceStockAsync(Guid productVariantId, Guid warehouseId, int quantity);

        // Increase stock (e.g. when order canceled or stock replenished)
        Task<bool> IncreaseStockAsync(Guid productVariantId, Guid warehouseId, int quantity);

        // Add or update inventory record
        Task AddOrUpdateInventoryAsync(Inventory inventory);      
    }
}
