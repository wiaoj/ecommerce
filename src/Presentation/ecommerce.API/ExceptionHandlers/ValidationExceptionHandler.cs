using ecommerce.Application.Exceptions;
using ecommerce.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.ExceptionHandlers;
public class ValidationExceptionHandler : IExceptionHandler {
    public async ValueTask<Boolean> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        if(exception is not ValidationException validationException)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails {
            Type = validationException.ErrorType,
            Title = $"Validation error{GetPluralSuffixBasedOnErrorCount(validationException.Errors.Count)}",
            Status = StatusCodes.Status400BadRequest,
            Detail = validationException.Message,
            Errors = validationException.Errors
        }, cancellationToken);
        return true;
    }

    private static String GetPluralSuffixBasedOnErrorCount(Int32 count) => count > 1 ? "s" : String.Empty;
}