using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using FluentValidation;

namespace ecommerce.Application.Validators.ProductValidators;
internal static class ProductValidatorExtensions {
    public static IRuleBuilderOptions<T, Guid> ProductExist<T>(this IRuleBuilder<T, Guid> ruleBuilder,
                                                                      IProductRepository productRepository) {
        async Task<Boolean> predicate(Guid id, CancellationToken cancellationToken) => await productRepository.ExistsAsync(ProductId.Create(id), cancellationToken);
        return ruleBuilder.MustAsync(predicate);
    }
}