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
        services.AddScoped<IProductAttributeService, ProductAttributeService>();
        services.AddScoped<IProductAttributeValueService, ProductAttributeValueService>();
        services.AddScoped<IProductGroupService, ProductGroupService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDeliveryWindowService, DeliveryWindowService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IImageTypeService, ImageTypeService>();
        services.AddScoped<IBankService, BankService>();
        services.AddScoped<IPostCodeService, PostCodeService>();


        return services;
    }
}