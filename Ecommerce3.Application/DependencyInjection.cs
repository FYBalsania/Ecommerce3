using Ecommerce3.Application.Services;
using Ecommerce3.Application.Services.Admin;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce3.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCommonApplicationServices()
        {
            services.AddScoped<IIPAddressService, IPAddressService>();

            return services;
        }

        public IServiceCollection AddAdminApplicationServices()
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IProductAttributeValueService, ProductAttributeValueService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDeliveryWindowService, DeliveryWindowService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageTypeService, ImageTypeService>();
            services.AddScoped<IKVPListItemService, KVPListItemService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IPostCodeService, PostCodeService>();
            services.AddScoped<ITextListItemService, TextListItemService>();
            services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddScoped<IProductGroupProductAttributeService, ProductGroupProductAttributeService>();

            return services;
        }

        public IServiceCollection AddStoreFrontApplicationServices()
        {
            services.AddScoped<Services.StoreFront.Interfaces.IPageService, Services.StoreFront.PageService>();
            services.AddScoped<Services.StoreFront.Interfaces.IProductService, Services.StoreFront.ProductService>();
            services.AddScoped<Services.StoreFront.Interfaces.ICategoryService, Services.StoreFront.CategoryService>();
            return services;
        }

        public IServiceCollection AddAPIServices()
        {
            services
                .AddScoped<Services.API.Interfaces.IProductAttributeService, Services.API.ProductAttributeService>();
            services
                .AddScoped<Services.API.Interfaces.IProductAttributeValueService,
                    Services.API.ProductAttributeValueService>();
            return services;
        }
    }
}