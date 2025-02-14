using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Application.Services.VendorServices;
using eCommerce.Domain.Entities;
using eCommerce.Domain.IdentityEntities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Dapper;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();



builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<eCommerce.Infrastructure.Data.eCommerceDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleStore<RoleStore<ApplicationRole, eCommerceDbContext, Guid>>();


builder.Services.AddDbContext<eCommerceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        context.Response.ContentType = "application/json";
//        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (error is ValidationException)
//        {
//            context.Response.StatusCode = StatusCodes.Status400BadRequest;
//            await context.Response.WriteAsync(new { Error = error.Message }.ToString());
//        }
//    });
//});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
