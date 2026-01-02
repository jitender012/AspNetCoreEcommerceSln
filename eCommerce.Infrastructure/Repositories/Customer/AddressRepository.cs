using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Customer;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Customer
{
    public class AddressRepository : IAddressRepository
    {
        private readonly eCommerceDbContext _context;

        public AddressRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Addresses
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.IsDefault)
                .ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int addressId, Guid userId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(x => x.AddressId == addressId && x.UserId == userId);
        }

        public async Task<Address?> GetDefaultAsync(Guid userId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(x => x.UserId == userId && x.IsDefault == true);
        }

        public async Task AddAsync(Address address)
        {
            // If first address → make default
            if (!await _context.Addresses.AnyAsync(x => x.UserId == address.UserId))
            {
                address.IsDefault = true;
            }

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int addressId, Guid userId)
        {
            var address = await GetByIdAsync(addressId, userId);
            if (address == null) return;

            bool wasDefault = address.IsDefault == true;

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            // If default deleted → set another as default
            if (wasDefault)
            {
                var nextAddress = await _context.Addresses
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                if (nextAddress != null)
                {
                    nextAddress.IsDefault = true;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task SetDefaultAsync(int addressId, Guid userId)
        {
            var addresses = await _context.Addresses
                .Where(x => x.UserId == userId)
                .ToListAsync();

            foreach (var addr in addresses)
                addr.IsDefault = addr.AddressId == addressId;

            await _context.SaveChangesAsync();
        }
    }

}
