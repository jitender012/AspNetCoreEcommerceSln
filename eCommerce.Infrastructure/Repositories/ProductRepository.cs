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
    public class ProductRepository :  BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(eCommerceDbContext context):base(context)
        {
            
        }
    }
}
