using ecommerce.Application.Common.Extensions;
using ecommerce.Domain.Extensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest {
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        if(this.validators.Any().IsFalse())
            return await next();

        ValidationContext<TRequest> validationContext = new(request);

        IEnumerable<Task<ValidationResult>> validationTasks =
            this.validators.Select(validator => validator.ValidateAsync(validationContext, cancellationToken));

        ValidationResult[] validationResults = await Task.WhenAll(validationTasks);

        Dictionary<String, String[]> failures = validationResults
            .SelectMany(result => result.Errors)
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(x => x.ErrorMessage).ToArray()
            );
        return failures.CountIsNotZero() ? throw new Exceptions.ValidationException(failures) : await next();
    }
}