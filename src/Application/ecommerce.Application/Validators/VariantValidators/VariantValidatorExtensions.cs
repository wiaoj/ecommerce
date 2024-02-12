using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using FluentValidation;

namespace ecommerce.Application.Validators.VariantValidators;
internal static class VariantValidatorExtensions {
    public static IRuleBuilderOptions<T, String> VariantNotExist<T>(this IRuleBuilder<T, String> ruleBuilder,
                                                                    IVariantRepository variantRepository) {

        async Task<Boolean> predicate(String name, CancellationToken cancellationToken) {
            return await variantRepository.ExistsByNameAsync(name, cancellationToken).IsFalse();
        }

        return ruleBuilder.MustAsync(predicate).WithMessage("Variant does not exist.");
    }

    public static IRuleBuilderOptions<T, Guid> VariantExist<T>(this IRuleBuilder<T, Guid> ruleBuilder,
                                                               IVariantRepository variantRepository) {

        async Task<Boolean> predicate(Guid id, CancellationToken cancellationToken) {
            return await variantRepository.ExistsAsync(VariantId.Create(id), cancellationToken);
        }

        return ruleBuilder.MustAsync(predicate).WithMessage("Variant Id does not exist.");
    }

    public static IRuleBuilderOptions<T, string> MustNotHaveExistingVariantOption<T>(this IRuleBuilder<T, string> ruleBuilder,
                                                                                     Func<T, VariantId> variantIdSelector,
                                                                                     IVariantRepository variantRepository) {

        async Task<Boolean> predicate(T command, String value, CancellationToken cancellationToken) {
            VariantId variantId = variantIdSelector(command);
            return await variantRepository.VariantOptionExists(variantId,
                                                               VariantOptionValue.Create(value),
                                                               cancellationToken).IsFalse();
        }

        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .MustAsync(predicate);
    }
}