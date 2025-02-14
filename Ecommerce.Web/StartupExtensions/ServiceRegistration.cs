using eCommerce.Application.Services;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Repositories.Products;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Application.Services.VendorServices;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Application.Services.ProductServices;

namespace eCommerce.Web.StartupExtensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {

            //Services dependency injection
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFeatureCategoryService, FeatureCategoryService>();
            services.AddScoped<IProductFeatureService, ProductFeatureService>();

            //for retrieving of user information
            services.AddScoped<IUserContextService, UserContextService>();

            //Services dependency resolve
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>(); 
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFeatureCategoryRepository, FeatureCategoryRepository>();
            services.AddScoped<IFeatureOptionRepository, FeatureOptionRepository>();
            services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();

        }
    }
}

