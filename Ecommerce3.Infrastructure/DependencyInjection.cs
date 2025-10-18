using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.QueryRepositories;
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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBrandPageRepository, BrandPageRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryPageRepository, CategoryPageRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        // services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        // services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        // services.AddScoped<ICustomerRepository, CustomerRepository>();
        // services.AddScoped<IDeliveryWindowRepository, DeliveryWindowRepository>();
        // services.AddScoped<IDiscountRepository, DiscountRepository>();
        // services.AddScoped<IImageRepository, ImageRepository>();
        // services.AddScoped<IPageRepository<Page>, PageRepository<Page>>();
        // services.AddScoped<IProductRepository, ProductRepository>();
        // services.AddScoped<IProductSpecificationGroupRepository, ProductSpecificationGroupRepository>();
        // services.AddScoped(typeof(ImageRepository<>), typeof(ImageRepository<>));
        // services.AddScoped<IImageRepository<Image>, ImageRepository<Image>>();
        // services.AddScoped<IBrandImageRepository, BrandImageRepository>();
        // services.AddScoped<ICategoryImageRepository, CategoryImageRepository>();
        // services.AddScoped<IProductImageRepository, ProductImageRepository>();
        // services.AddScoped<IProductGroupImageRepository, ProductGroupImageRepository>();

        services.AddScoped<IBrandQueryRepository, BrandQueryRepository>();
        // services.AddScoped<IPageQueryRepository, PageQueryRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<IProductAttributeQueryRepository, ProductAttributeQueryRepository>();

        return services;
    }
}