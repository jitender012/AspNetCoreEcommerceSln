using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Application.Services;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Application.Services.ProductServices;
using eCommerce.Application.Services.VendorServices;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Products;

namespace eCommerce.Web.StartupExtensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {

            //Services dependency injection
            services.AddScoped<IFileUploadService, FileUploadService>();           
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IProductCategoryService, CategoryService>();            
            services.AddScoped<IFeatureCategoryService, FeatureCategoryService>();
            services.AddScoped<IProductFeatureService, ProductFeatureService>();
            services.AddScoped<IFeatureOptionService, FeatureOptionService>();

            //for retrieving of user information
            services.AddScoped<IUserContextService, UserContextService>();

            //Services dependency resolve
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>(); 
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFeatureCategoryRepository, FeatureCategoryRepository>();
            services.AddScoped<IFeatureOptionRepository, FeatureOptionRepository>();
            services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();           
            services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
            services.AddScoped<IProductConfigurationRepository, ProductConfigurationRepository>();
        }
    }
}

