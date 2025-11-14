using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ExceptionHandlers;

public sealed class ArgumentOutOfRangeExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ArgumentOutOfRangeExceptionHandler> _logger;

    public ArgumentOutOfRangeExceptionHandler(ILogger<ArgumentOutOfRangeExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception.GetType() != typeof(ArgumentOutOfRangeException)) return false;
        
        var argumentOutOfRangeException = exception as ArgumentOutOfRangeException;
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = argumentOutOfRangeException!.Message,
            Extensions =
            {
                { argumentOutOfRangeException.ParamName!, argumentOutOfRangeException.Message }
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}