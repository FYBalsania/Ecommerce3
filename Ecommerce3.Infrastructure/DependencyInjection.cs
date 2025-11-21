using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Policies;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Imaging;
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

        services.AddSingleton<IImageTypeDetector, FileSignatureImageTypeDetector>();

        services.AddScoped<IBankImageRepository, BankImageRepository>();
        services.AddScoped<IBankPageRepository, BankPageRepository>();
        services.AddScoped<IBankRepository, BankRepository>();

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandQueryRepository, BrandQueryRepository>();
        services.AddScoped<IBrandPageRepository, BrandPageRepository>();
        services.AddScoped<IImageQueryRepository, BrandImageQueryRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryPageRepository, CategoryPageRepository>();
        services.AddScoped<IImageQueryRepository, CategoryImageQueryRepository>();

        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        services.AddScoped<IProductAttributeQueryRepository, ProductAttributeQueryRepository>();

        services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        services.AddScoped<IProductGroupQueryRepository, ProductGroupQueryRepository>();
        services.AddScoped<IProductGroupPageRepository, ProductGroupPageRepository>();
        services.AddScoped<IImageQueryRepository, ProductGroupImageQueryRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
        services.AddScoped<IProductPageRepository, ProductPageRepository>();

        services.AddScoped<IDeliveryWindowRepository, DeliveryWindowRepository>();
        services.AddScoped<IDeliveryWindowQueryRepository, DeliveryWindowQueryRepository>();

        services.AddScoped<IImageTypeRepository, ImageTypeRepository>();
        services.AddScoped<IImageTypeQueryRepository, ImageTypeQueryRepository>();

        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IBankQueryRepository, BankQueryRepository>();
        services.AddScoped<IImageQueryRepository, BankImageQueryRepository>();

        services.AddScoped<IPostCodeRepository, PostCodeRepository>();
        services.AddScoped<IPostCodeQueryRepository, PostCodeQueryRepository>();

        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<IBrandRepository>());
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<ICategoryRepository>());
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<IBankRepository>());
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<IProductGroupRepository>());

        services.AddScoped<IImageRepository<Image>, ImageRepository<Image>>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}