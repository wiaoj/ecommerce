using Microsoft.AspNetCore.Diagnostics;

namespace ecommerce.API.ExceptionHandlers;
public class ExceptionHandler : IExceptionHandler {
    public async ValueTask<Boolean> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync("Someting went wrong..", cancellationToken);
        return true;
    }
}

public class DevelopmentExceptionHandler : IExceptionHandler {
    public async ValueTask<Boolean> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(exception, cancellationToken);
        return true;
    }
}