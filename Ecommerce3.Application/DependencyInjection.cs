using Ecommerce3.Application.Services;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce3.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Mappings.BrandProfile).Assembly);
        services.AddScoped<IBrandService, BrandService>();
        
        
        return services;
    }
}