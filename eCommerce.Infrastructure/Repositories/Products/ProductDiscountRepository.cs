using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class ProductDiscountRepository : IProductDiscountRepository
    {
        public Task<bool> DeleteDiscount(ProductDiscount discount)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDiscount> GetDiscountDetails(int discountId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertDiscount(ProductDiscount discount)
        {
            throw new NotImplementedException();
        }
    }
}
