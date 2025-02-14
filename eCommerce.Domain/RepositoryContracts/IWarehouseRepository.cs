using eCommerce.Domain.Entities;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        Task AddMultipleProducts(IEnumerable<eCommerce.Domain.Entities.Product> items);
    }
}
