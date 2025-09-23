using Microsoft.AspNetCore.Http;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IIPAddressService
{
    public string GetClientIpAddress(HttpContext context);
}