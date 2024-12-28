using eCommerce.Application.DTO;
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
        Task<WarehouseDTO> GetStoreByVendorId(int id);
        Task<int> AddStore(WarehouseDTO data);
        bool UpdateStore(WarehouseDTO data);
        bool DeleteStore(int id);
    }
}
