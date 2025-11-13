using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce3.Admin.ExceptionHandlers;

public sealed class ArgumentNullExceptionHandler :IExceptionHandler
{
    private readonly ILogger<ArgumentNullExceptionHandler> _logger;

    public ArgumentNullExceptionHandler(ILogger<ArgumentNullExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}