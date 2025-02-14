using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class FeatureOptionRepository : IFeatureOptionRepository
    {
        private readonly eCommerceDbContext _context;

        public FeatureOptionRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task InsertFeatureOptionsAsync(FeatureOption featureOption)
        {
            try
            {
                await _context.FeatureOptions.AddAsync(featureOption);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong.", ex);
            }
        }

        public async Task<List<FeatureOption>> GetFeatureOptionsAsync()
        {
            try
            {
                return await _context.FeatureOptions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong.", ex);
            }
        }

        public async Task<List<FeatureOption>> GetFeatureOptionsByProductFeatureAsync(int productFeatureId)
        {

            try
            {
                if (productFeatureId <= 0)
                    throw new ArgumentException("ProductFeatureId must be greater than zero.", nameof(productFeatureId));

                return await _context.FeatureOptions
                    .Where(pf => pf.ProductFeatureId == productFeatureId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong.", ex);
            }
        }
    }
}
