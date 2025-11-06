using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Seller;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Seller
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly eCommerceDbContext _context;
        public WarehouseRepository(eCommerceDbContext context)
        {
            _context = context;
        }
        public async Task<List<Warehouse>> FetchAllAsync()
        {
            return await _context.Warehouses
                .ToListAsync();
        }

        public async Task<List<Warehouse>> FetchBySellerIdAsync(Guid sellerId)
        {
            return await _context.Warehouses
                .Where(x => x.UserId == sellerId)
                .ToListAsync();
        }

        public async Task<Warehouse> FetchByIdAsync(int id)
        {
            var warehouse = await _context.Warehouses
                .Where(x => x.WarehouseId == id)
                .FirstOrDefaultAsync();
            
            if (warehouse == null)
            {
                return new Warehouse();
            }
            return warehouse;
        }

        public Task<int> InsertAsync(Warehouse warehouse)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyAsync(Warehouse warehouse)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int warehouseId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
