using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ExceptionHandlers;

public class DomainExceptionHandler(ILogger<DomainExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception.GetType() != typeof(DomainException)) return false;

        var domainException = exception as DomainException;
        var validationProblemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Validation errors occurred.",
            Detail = domainException!.Message,
            Errors =
            {
                { domainException.Error.Code, [domainException.Error.Message] },
            }
        };

        httpContext.Response.StatusCode = validationProblemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);

        return true;
    }
}