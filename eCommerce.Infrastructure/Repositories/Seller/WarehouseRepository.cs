using Dapper;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Seller;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Seller
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly eCommerceDbContext _context;
        private readonly DapperContext _dapperContext;
        public WarehouseRepository(eCommerceDbContext context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }
        public async Task<List<Warehouse>> FetchAllAsync()
        {
            string query = "SELECT * FROM Warehouse";

            using (var connection = _dapperContext.CreateConnection())
            {
                var warehouses = await connection.QueryAsync<Warehouse>(query);
                return warehouses.ToList();
            }
        }

        public async Task<List<Warehouse>> FetchBySellerIdAsync(Guid sellerId)
        {
            string query = "SELECT * FROM [Inventory].[Warehouse] where UserId = @sellerId";

            using (var connection = _dapperContext.CreateConnection())
            {
                var warehouses = await connection.QueryAsync<Warehouse>(query, new { sellerId });
                return warehouses.ToList();
            }
        }

        public async Task<Warehouse> FetchByIdAsync(Guid id)
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

        public async Task<Guid> InsertAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();

            return warehouse.WarehouseId;
        }

        public Task<bool> ModifyAsync(Warehouse warehouse)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid warehouseId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Warehouse>> GetAvailableWarehousesForVariantQuery(Guid variantId)
        {
            var result = await _context.Warehouses
                .Where(w => !w.Inventories
                .Any(i => i.ProductVariantId == variantId))
                .ToListAsync();
            return result;
        }
    }
}
