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
    public class OrderRepository : IOrderRepository
    {
        private readonly eCommerceDbContext _context;
        public OrderRepository(eCommerceDbContext dbContext)
        {
            _context = dbContext;
        }
    
        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            return await _context.Orders
              .Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.ProductVariant)
              .Where(o => o.CustomerId == customerId)
              .OrderByDescending(o => o.CreatedAt)
              .ToListAsync(); 
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<int> GetTotalOrdersCountAsync(Guid customerId)
        {
            return await _context.Orders.CountAsync(o => o.CustomerId == customerId);
        }

        public async Task<bool> OrderBelongsToCustomerAsync(Guid orderId, Guid customerId)
        {
            return await _context.Orders.AnyAsync(o => o.OrderId == orderId && o.CustomerId == customerId);
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null)
            {
                order.OrderStatus = newStatus;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
