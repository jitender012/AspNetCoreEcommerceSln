using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        private readonly new eCommerceDbContext _context;
        public CategoryRepository(eCommerceDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetMainCategories()
        {            
            return await _context.Categories.Where(x => x.ParentCategoryId < 1).ToListAsync();
        }

        public async Task<List<Category>> GetSubCategories()
        {
            return await _context.Categories.Where(x => x.ParentCategoryId > 0).ToListAsync();
        }
    }
}
