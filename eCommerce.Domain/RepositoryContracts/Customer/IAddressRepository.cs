using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Customer
{
    public interface IAddressRepository
    {
        // Get
        Task<List<Address>> GetByUserIdAsync(Guid userId);
        Task<Address?> GetByIdAsync(int addressId, Guid userId);

        // Create
        Task AddAsync(Address address);

        // Update
        Task UpdateAsync(Address address);

        // Delete
        Task DeleteAsync(int addressId, Guid userId);

        // Default address handling (very useful)
        Task<Address?> GetDefaultAsync(Guid userId);
        Task SetDefaultAsync(int addressId, Guid userId);
    }
}
