using eCommerce.Application.Behaviors;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Validators;
using eCommerce.Domain.IdentityEntities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Dapper;
using eCommerce.Infrastructure.Data;
using eCommerce.Web.Hubs;
using eCommerce.Web.StartupExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data;

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

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateBrandCommand).Assembly);
});

builder.Services.AddValidatorsFromAssemblyContaining<BrandSaveValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<eCommerce.Infrastructure.Data.eCommerceDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleStore<RoleStore<ApplicationRole, eCommerceDbContext, Guid>>();

builder.Services.AddHttpContextAccessor();

//Add support to logging with SERILOG
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSignalR();

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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapHub<OrderHub>("/orderHub");

app.Run();
