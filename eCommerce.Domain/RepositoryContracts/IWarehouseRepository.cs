using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        Task AddMultipleProducts(IEnumerable<Product> items);
    }
}
