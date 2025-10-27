using Ecommerce3.Application.Services;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProductGroupService = Ecommerce3.Application.Services.ProductGroupService;

namespace Ecommerce3.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IIPAddressService, IPAddressService>();
        services.AddScoped<IBrandService, BrandService>();
        // services.AddScoped<IPageService, PageService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IProductAttributeService, ProductAttributeService>();
        services.AddScoped<IProductGroupService, ProductGroupService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDeliveryWindowService, DeliveryWindowService>();

        return services;
    }
}