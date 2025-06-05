using eCommerce.Domain.CustomException;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class FeatureOptionRepository : IFeatureOptionRepository
    {
        private readonly eCommerceDbContext _context;
        private readonly ILogger<FeatureOptionRepository> _logger;

        public FeatureOptionRepository(eCommerceDbContext context, ILogger<FeatureOptionRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> InsertAsync(FeatureOption featureOption)
        {
            try
            {
                await _context.FeatureOptions.AddAsync(featureOption);
                await _context.SaveChangesAsync();
                return featureOption.FeatureOptionId;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Database error fetching user.");
                throw new Exception("Database related error.", ex);
            }
        }

        public async Task<List<FeatureOption>> FetchAllAsync()
        {
            try
            {
                return await _context.FeatureOptions.Include(fo => fo.ProductFeature.Name).ToListAsync();
            }
            catch (DbException ex)
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

        public async Task<bool> RemoveFeatureOptionAsync(int id)
        {
            if (id <= 0)
            {
                return false;
                throw new ArgumentException("Feature option id must be greater than zero.", nameof(id));
            }

            try
            {
                var record = await _context.FeatureOptions
                                    .Where(pf => pf.FeatureOptionId == id).FirstOrDefaultAsync();

                if (record == null)
                {
                    throw new NotFoundException($"Feature option with id {id} not found");
                }

                _context.FeatureOptions.Remove(record);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                _logger.LogError("Database operation error.");
                return false;
                throw new Exception("Database operation error.", ex);
            }
        }
    }
}
