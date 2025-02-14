using eCommerce.Application.DTO;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.VendorServiceContracts
{
    public interface IWarehouseService
    {
        Task<List<WarehouseDTO>> GetAllStores();
        Task<WarehouseDTO> GetStoreById(int id);
        Task<List<WarehouseDTO>> GetStoresByVendorId(Guid id);
        Task<int> AddStore(WarehouseDTO data);
        Task<bool> UpdateWarehouse(WarehouseDTO data);
        Task<bool> DeleteStore(int id);

        Task<IEnumerable<Product>> GetOutOfStockProductsAsync();

        Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
    }
}
