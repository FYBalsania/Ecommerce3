using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
        if (exception.GetType() != typeof(ArgumentNullException)) return false;
        
        var argumentNullException = exception as ArgumentNullException;
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = argumentNullException!.Message,
            Extensions =
            {
                { argumentNullException.ParamName!, argumentNullException.Message }
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}