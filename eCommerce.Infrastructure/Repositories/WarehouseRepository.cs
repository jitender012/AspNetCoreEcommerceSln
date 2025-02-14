using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Data;

namespace eCommerce.Infrastructure.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse>, IWarehouseRepository

    {
        public WarehouseRepository(eCommerceDbContext context) : base(context)
        {

        }

        public Task AddMultipleProducts(IEnumerable<ProductDiscount> items)
        {
            throw new NotImplementedException();
        }

        public Task AddMultipleProducts(IEnumerable<Domain.Entities.Product> items)
        {
            throw new NotImplementedException();
        }
    }
}
