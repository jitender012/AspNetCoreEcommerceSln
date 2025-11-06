using eCommerce.Domain.CustomException;
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
    public class WishlistRepository : IWishlistRepository
    {
        private readonly eCommerceDbContext _context;
        public WishlistRepository(eCommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Wishlist> FetchByIdAsync(int id)
        {
            var item = await _context.Wishlists.Where(x => x.WishlistId == id).FirstOrDefaultAsync();
            if (item == null)
                return new Wishlist();

            return item;
        }

        public async Task<List<Wishlist>> FetchByCustomerIdAsync(Guid customerId)
        {
            var items = await _context.Wishlists
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            return items;
        }

        public async Task<int> InsertAsync(Wishlist wishlist)
        {
            await _context.Wishlists.AddAsync(wishlist);

            return wishlist.WishlistId;
        }

        public async Task<bool> ModifyAsync(Wishlist wishlist)
        {
            var item = _context.Wishlists
                .FirstOrDefault(x => x.WishlistId == wishlist.WishlistId);
            if (item == null)
                return false;

            item = wishlist;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int wishlistId, Guid userId)
        {
            var item = await _context.Wishlists
                .FirstOrDefaultAsync(x => x.WishlistId == wishlistId);

            if (item == null)
                return false;

            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
      
    }
}
