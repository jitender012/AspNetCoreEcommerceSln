using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IProductDiscountRepository
    {
        Task<bool> InsertDiscount(ProductDiscount discount);
        Task<ProductDiscount> GetDiscountDetails(int discountId);
        Task<bool> DeleteDiscount(ProductDiscount discount);

    }
}
