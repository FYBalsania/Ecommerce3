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
        // services.AddScoped<IPageQueryRepository, PageQueryRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandQueryRepository, BrandQueryRepository>();
        services.AddScoped<IBrandPageRepository, BrandPageRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryPageRepository, CategoryPageRepository>();

        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        services.AddScoped<IProductAttributeQueryRepository, ProductAttributeQueryRepository>();

        services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        services.AddScoped<IProductGroupQueryRepository, ProductGroupQueryRepository>();
        services.AddScoped<IProductGroupPageRepository, ProductGroupPageRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
        services.AddScoped<IProductPageRepository, ProductPageRepository>();
        
        return services;
    }
}