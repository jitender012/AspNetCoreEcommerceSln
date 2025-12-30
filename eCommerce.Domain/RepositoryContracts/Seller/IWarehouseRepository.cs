using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Seller
{
    public interface IWarehouseRepository
    {
     
        Task<Warehouse> FetchByIdAsync(Guid Guid);
        Task<List<Warehouse>> FetchBySellerIdAsync(Guid sellerId);
        Task<List<Warehouse>> FetchAllAsync();
        Task<Guid> InsertAsync(Warehouse warehouse);
        Task<bool> ModifyAsync(Warehouse warehouse);
        Task<bool> RemoveAsync(Guid warehouseId, Guid userId);

        /// <summary>
        /// Get available warehouses where variant has not been stocked yet
        /// </summary>
        /// <param name="variantId"></param>
        /// <returns></returns>
        Task<List<Warehouse>> GetAvailableWarehousesForVariantQuery(Guid variantId);
    }
}
