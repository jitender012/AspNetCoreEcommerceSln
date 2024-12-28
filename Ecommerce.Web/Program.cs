using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using eCommerce.Application.Services;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Domain.IdentityEntities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Dapper;
using eCommerce.Infrastructure.Models;
using eCommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using eCommerce.Web.StartupExtensions;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://your-api-url.com/api/");
});

builder.Services.AddApplicationService();


//Registering DbContext
builder.Services.AddDbContext<eCommerceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDapperRepository, DapperRepository>();

builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<eCommerce.Infrastructure.Models.eCommerceDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleStore<RoleStore<ApplicationRole, eCommerceDbContext, Guid>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "adminRoute",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" });


app.MapControllerRoute(
    name: "vendorRoute",
    pattern: "Vendor/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Vendor" });

app.UseAuthorization();


app.Run();
