using FluentValidation;
using MediatR;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class ErrorHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> {
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        try {
            // İstek işleme
            return await next();
        }
        catch(ValidationException validationException) {
            // ValidationException özel işleme
            // Örneğin: Hata detaylarını birleştirip döndürme
            IEnumerable<String> errorMessages = validationException.Errors.Select(e => e.ErrorMessage);
            String detailedErrorMessage = String.Join("; ", errorMessages);
            throw;
        }
        catch(Exception) {
            // Hata işleme 
            throw; // veya özel bir hata dönüşü yapabilirsiniz
        }
    }
}