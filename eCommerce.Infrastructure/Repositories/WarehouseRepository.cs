using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse>, IWarehouseRepository

    {
        public WarehouseRepository(eCommerceDbContext context) : base(context)
        {

        }

        public Task AddMultipleProducts(IEnumerable<Product> items)
        {
            throw new NotImplementedException();
        }
    }
}
