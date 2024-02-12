using ecommerce.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.ExceptionHandlers;
public class NotFoundExceptionHandler : IExceptionHandler {
    public async ValueTask<Boolean> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        if(exception is not NotFoundException notFoundException)
            return false;

        ProblemDetails problemDetails = new() {
            Type = notFoundException.ErrorType,
            Title = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Detail = notFoundException.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}