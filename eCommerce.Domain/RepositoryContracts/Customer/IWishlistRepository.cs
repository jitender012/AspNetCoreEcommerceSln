using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Customer
{
    public interface IWishlistRepository
    {
        Task<int> InsertAsync(Wishlist wishlist);
        Task<bool> ModifyAsync(Wishlist wishlist);
        Task<bool> RemoveAsync(int wishlistId, Guid userId);
        Task<Wishlist> FetchByIdAsync(int id);
        Task<List<Wishlist>> FetchByCustomerIdAsync(Guid customerId);
    }
}
