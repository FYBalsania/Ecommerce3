using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce3.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        // services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        // services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        // services.AddScoped<ICustomerRepository, CustomerRepository>();
        // services.AddScoped<IDeliveryWindowRepository, DeliveryWindowRepository>();
        // services.AddScoped<IDiscountRepository, DiscountRepository>();
        // services.AddScoped<IImageRepository, ImageRepository>();
        // services.AddScoped<IPageRepository, PageRepository>();
        // services.AddScoped<IProductRepository, ProductRepository>();
        // services.AddScoped<IProductSpecificationGroupRepository, ProductSpecificationGroupRepository>();

        return services;
    }
}