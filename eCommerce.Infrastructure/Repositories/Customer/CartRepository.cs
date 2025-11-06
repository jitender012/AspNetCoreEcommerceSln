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
    public class CartRepository : ICartRepository
    {
        private readonly eCommerceDbContext _context;
        public CartRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByCustomerIdAsync(Guid customerId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.Status == "Active");
        }


        public async Task<CartItem?> GetCartItemAsync(Guid customerId, Guid productVariantId)
        {
            return await _context.CartItems
                .Where(x => x.Cart.CustomerId == customerId && x.ProductVariantId == productVariantId)
                .FirstOrDefaultAsync();
        }
        public async Task<Guid> CreateCartAsync(Guid customerId)
        {
            Cart cart = new Cart()
            {
                CustomerId = customerId,
                CartId = Guid.NewGuid(),
                CartItems = new List<CartItem>(),
                CreatedAt = DateTime.UtcNow,
                Status = "active"
            };
            try
            {
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();

                return cart.CartId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem.CartItemId;
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {

            var existingCartItem = _context.CartItems
               .FirstOrDefault(ci => ci.CartItemId == cartItem.CartItemId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = cartItem.Quantity;
                existingCartItem.TotalPrice = cartItem.TotalPrice;

                _context.CartItems.Update(existingCartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.Where(x => x.CartItemId == cartItemId)
                .FirstOrDefaultAsync();

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(Guid customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.Status == "Active");

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetCartTotalAsync(Guid customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.Status == "Active");

            return cart?.CartItems.Sum(ci => ci.TotalPrice) ?? 0;
        }
       
    }
}
