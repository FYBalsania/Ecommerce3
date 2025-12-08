using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Policies;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.EFCoreInterceptors;
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
        services.AddSingleton<DeleteInterceptor>();
        services.AddSingleton<IImageTypeDetector, FileSignatureImageTypeDetector>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .AddInterceptors(sp.GetRequiredService<DeleteInterceptor>());
        });

        //Repositories.
        services.AddScoped<IBankImageRepository, BankImageRepository>();
        services.AddScoped<IBankPageRepository, BankPageRepository>();
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandPageRepository, BrandPageRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryPageRepository, CategoryPageRepository>();
        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        services.AddScoped<IProductAttributeQueryRepository, ProductAttributeQueryRepository>();
        services.AddScoped<IProductAttributeColourValueRepository, ProductAttributeColourValueRepository>();
        services.AddScoped<IProductAttributeDecimalValueRepository, ProductAttributeDecimalValueRepository>();
        services.AddScoped<IProductAttributeDateOnlyValueRepository, ProductAttributeDateOnlyValueRepository>();
        services.AddScoped<IProductAttributeBooleanValueRepository, ProductAttributeBooleanValueRepository>();
        services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        services.AddScoped<IProductGroupPageRepository, ProductGroupPageRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductPageRepository, ProductPageRepository>();
        services.AddScoped<IDeliveryWindowRepository, DeliveryWindowRepository>();
        services.AddScoped<IImageTypeRepository, ImageTypeRepository>();
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IPostCodeRepository, PostCodeRepository>();
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<IBrandRepository>());
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<ICategoryRepository>());
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<IBankRepository>());
        services.AddScoped<IImageEntityRepository>(sp => sp.GetRequiredService<IProductGroupRepository>());
        services.AddScoped<IImageRepository<Image>, ImageRepository<Image>>();
        services.AddScoped<ITextListItemRepository, TextListItemRepository>();
        services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();

        //Query Repositories.
        services.AddScoped<IBankQueryRepository, BankQueryRepository>();
        services.AddScoped<IImageQueryRepository, BankImageQueryRepository>();
        services.AddScoped<IImageQueryRepository, ProductGroupImageQueryRepository>();
        services.AddScoped<IImageQueryRepository, BrandImageQueryRepository>();
        services.AddScoped<IImageQueryRepository, CategoryImageQueryRepository>();
        services.AddScoped<IBrandQueryRepository, BrandQueryRepository>();
        services.AddScoped<IProductAttributeValueQueryRepository, ProductAttributeValueQueryRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
        services.AddScoped<IPostCodeQueryRepository, PostCodeQueryRepository>();
        services.AddScoped<IImageTypeQueryRepository, ImageTypeQueryRepository>();
        services.AddScoped<IPageQueryRepository, PageQueryRepository>();
        services.AddScoped<IDeliveryWindowQueryRepository, DeliveryWindowQueryRepository>();
        services.AddScoped<IProductGroupQueryRepository, ProductGroupQueryRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ITextListItemQueryRepository, TextListItemQueryRepository>();
        services.AddScoped<IProductTextListItemQueryRepository, ProductTextListItemQueryRepository>();
        services.AddScoped<IUnitOfMeasureQueryRepository, UnitOfMeasureQueryRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}