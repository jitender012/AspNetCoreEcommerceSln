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
     
        Task<Warehouse> FetchByIdAsync(int id);
        Task<List<Warehouse>> FetchBySellerIdAsync(Guid sellerId);
        Task<List<Warehouse>> FetchAllAsync();
        Task<int> InsertAsync(Warehouse warehouse);
        Task<bool> ModifyAsync(Warehouse warehouse);
        Task<bool> RemoveAsync(int warehouseId, Guid userId);
    }
}
