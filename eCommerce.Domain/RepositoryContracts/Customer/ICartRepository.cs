using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts.Customer
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByCustomerIdAsync(Guid customerId);        
        Task<Guid> CreateCartAsync(Guid customerId);
        Task<int> AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);

        /// <summary>
        /// Fetch an item to check if it already exists
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productVariantId"></param>
        /// <returns></returns>
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productVariantId);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(Guid customerId);
        Task<decimal> GetCartTotalAsync(Guid customerId);
    }
}
