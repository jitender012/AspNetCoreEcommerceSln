using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Domain.CustomException;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _storeRepository;
        private readonly IDapperRepository _dapperRepository;
        public WarehouseService(IWarehouseRepository storeRepository, IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
            _storeRepository = storeRepository;
        }

        public Task<int> AddStore(WarehouseDTO data)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WarehouseDTO>> GetAllStores()
        {
            //string query = "SELECT * FROM Inventory.Warehouse";
            //var storeList = await _dapperRepository.QueryAsync<Warehouse>(query);

            var storeList = await _storeRepository.GetAllAsync();
            return WarehouseDTO.FromStores(storeList);
        }

        public async Task<WarehouseDTO> GetStoreById(int id)
        {
            if (id < 1)
            {
                throw new ValidationException("Id must be greater than 0");
            }
            //string query = "SELECT * FROM [Inventory].[Warehouse] WHERE WarehouseId = @Id";
            //var warehouse = await _dapperRepository.GetTAsync<Warehouse>(query, new { Id = id });

            var warehouse = await _storeRepository.SingleOrDefaultAsync(temp=>temp.WarehouseId== id);

            if (warehouse == null)
            {                                
                throw new NotFoundException($"Warehouse with ID {id} not found.");
            }
            return WarehouseDTO.FromStore(warehouse);
        }

        public Task<WarehouseDTO> GetStoreByVendorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStore(WarehouseDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
