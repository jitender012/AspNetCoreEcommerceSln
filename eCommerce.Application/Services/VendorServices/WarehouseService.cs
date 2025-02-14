using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Domain.CustomException;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerce.Application.Services.VendorServices
{
    public class WarehouseService(IWarehouseRepository warehouseRepository, ILogger<WarehouseService> logger) : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
        private readonly ILogger<WarehouseService> _logger = logger;

        public async Task<int> AddStore(WarehouseDTO data)
        {
            try
            {
                var warehouse = data.ToWarehouse();
                var newWarehouse = await _warehouseRepository.InsertAsync(warehouse);

                return newWarehouse.WarehouseId;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Null argument passed while creating Warehouse: {Name}.", data.Name);

                throw new ApplicationException("Warehouse data can not be null.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while creating Warehouse: {Name}.", data.Name);

                throw new ApplicationException("Unable to save warehouse to the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occured while creating Warehouse: {Name}.", data.Name);

                throw new ApplicationException("An unexpected error occured.", ex);
            }
        }

        public async Task<bool> DeleteStore(int id)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(id);
                if (warehouse != null)
                {
                    await _warehouseRepository.DeleteAsync(w => w.WarehouseId == warehouse.WarehouseId);
                    return true;
                }
                return false;
            }

            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while deleting Warehouse: {id}.", id);

                throw new ApplicationException("Unable to delete warehouse to the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error while deleting Warehouse: {Name}.", id);

                throw new ApplicationException("An unexpected error occured.", ex);
            }
        }

        public async Task<List<WarehouseDTO>> GetAllStores()
        {
            //string query = "SELECT * FROM Inventory.Warehouse";
            //var storeList = await _dapperRepository.QueryAsync<Warehouse>(query);
            try
            {
                var storeList = await _warehouseRepository.GetAllAsync();
                return WarehouseDTO.FromWarehouseList(storeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving Warehouse.");
                throw new ApplicationException("An unexpected error occured.", ex);
            }
        }

        public Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<WarehouseDTO> GetStoreById(int id)
        {
            if (id < 1)
            {
                throw new ValidationException("Id must be greater than 0");
            }
            //string query = "SELECT * FROM [Inventory].[Warehouse] WHERE WarehouseId = @Id";
            //var warehouse = await _dapperRepository.GetTAsync<Warehouse>(query, new { Id = id });

            var warehouse = await _warehouseRepository.SingleOrDefaultAsync(temp => temp.WarehouseId == id);

            return warehouse == null ? throw new NotFoundException($"Warehouse with ID {id} not found.")
                : WarehouseDTO.FromWarehouse(warehouse);
        }

        public async Task<List<WarehouseDTO>> GetStoresByVendorId(Guid id)
        {

            if (id.Equals(null))
            {
                throw new ValidationException("Vendor Id is null or invalid.");
            }

            var warehouseList = await _warehouseRepository
                            .GetAllAsync(w => w.UserId == id);

            return warehouseList == null ? throw new NotFoundException($"No Warehouse found.")
                : WarehouseDTO.FromWarehouseList(warehouseList);
        }

        public async Task<bool> UpdateWarehouse(WarehouseDTO data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Brand data cannot be null.");
            }

            try
            {
                // Fetch the existing brand from the database
                var warehouse = await _warehouseRepository.GetByIdAsync(data.WarehouseId);
                if (warehouse == null)
                {
                    _logger?.LogWarning("Warehouse with ID {WarehouseId} not found for update.", data.WarehouseId);
                    return false;
                }

                // Update the brand properties
                warehouse = data.ToWarehouse();

                await _warehouseRepository.UpdateAsync(warehouse);

                _logger?.LogInformation("Warehouse with ID {WarehouseId} successfully updated.", data.WarehouseId);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while updating Warehouse with ID {WarehouseId}.", data.WarehouseId);
                throw new ApplicationException("An error occurred while updating the Warehouse. Please try again later.", ex);
            }
        }

    }
}
