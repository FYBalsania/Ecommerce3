using Ecommerce3.Application.Services;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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

        return services;
    }
}