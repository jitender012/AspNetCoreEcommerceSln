using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;

namespace eCommerce.Infrastructure.Repositories.Common
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
