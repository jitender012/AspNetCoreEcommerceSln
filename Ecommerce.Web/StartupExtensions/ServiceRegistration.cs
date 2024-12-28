using eCommerce.Application.Services;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;

namespace eCommerce.Web.StartupExtensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {

            //Services dependency injection
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            //Services dependency resolve
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        }
    }
}

