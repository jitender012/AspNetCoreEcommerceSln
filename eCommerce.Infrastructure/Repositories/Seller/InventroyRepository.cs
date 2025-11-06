using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Seller
{
    public class InventroyRepository : IInventroyRepository
    {
        private readonly eCommerceDbContext _context;
        public InventroyRepository()
        {
            
        }
        public async Task<IEnumerable<Inventory>> GetByProductVariantIdAsync(Guid productVariantId)
        {
            return await _context.Inventories
               .Include(i => i.Warehouse)
               .Where(i => i.ProductVariantId == productVariantId)
               .ToListAsync();
        }

        public Task<Inventory?> GetByVariantAndWarehouseAsync(Guid productVariantId, Guid warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalStockAsync(Guid productVariantId)
        {
            throw new NotImplementedException();
        }

        public Task<Inventory?> GetWarehouseWithAvailableStockAsync(Guid productVariantId, int requiredQty)
        {
            throw new NotImplementedException();
        }
        public Task AddOrUpdateInventoryAsync(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncreaseStockAsync(Guid productVariantId, Guid warehouseId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReduceStockAsync(Guid productVariantId, Guid warehouseId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
