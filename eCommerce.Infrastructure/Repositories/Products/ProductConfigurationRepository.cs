using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class ProductConfigurationRepository : IProductConfigurationRepository
    {
        private readonly eCommerceDbContext _context;
        public ProductConfigurationRepository(eCommerceDbContext context)
        {
            _context = context;
        }

    }
}
