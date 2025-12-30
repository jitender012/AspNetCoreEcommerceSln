using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Seller
{
    public class InventroyRepository : IInventroyRepository
    {
        private readonly eCommerceDbContext _context;
        public InventroyRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetBySellerIdAsync(Guid sellerId)
        {
            var inventories = await _context.Inventories
                .Where(x=>x.Warehouse.UserId == sellerId)
                .Include(i => i.Warehouse)
                .Include(i => i.ProductVariant)
                .ToListAsync();
            return inventories;
        }

        public async Task<IEnumerable<Inventory>> GetByVariantIdAsync(Guid productVariantId)
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

        public async Task AddVariantAsync(Inventory inventory)
        {
            try
            {
                var existing = _context.Inventories.ToList();
                if (existing.Any(x => x.ProductVariantId == inventory.ProductVariantId && x.WarehouseId == inventory.WarehouseId))
                {
                    throw new InvalidOperationException("Variant is already in warehouse");
                }
                await _context.AddAsync(inventory);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception("Database error.", ex);
            }
        
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
