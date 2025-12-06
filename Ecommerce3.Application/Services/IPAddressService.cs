using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce3.Application.Services;

internal sealed class IPAddressService : IIPAddressService
{
    public string GetClientIpAddress(HttpContext context)
    {
        string? ip = null;
    
        // Try X-Forwarded-For header
        var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedHeader))
        {
            ip = forwardedHeader.Split(',')[0].Trim();
        }
    
        // If not found in X-Forwarded-For, try RemoteIpAddress
        if (string.IsNullOrEmpty(ip) && context.Connection.RemoteIpAddress != null)
        {
            ip = context.Connection.RemoteIpAddress.ToString();
        }
    
        return ip ?? "Unknown";
    }
}