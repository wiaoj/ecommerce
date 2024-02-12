using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using FluentValidation;

namespace ecommerce.Application.Validators.VariantValidators;
internal static class OptionValidatorExtensions {
    public static IRuleBuilderOptions<T, List<Guid>> OptionsExist<T>(this IRuleBuilder<T, List<Guid>> ruleBuilder,
                                                                 IVariantRepository variantRepository) {
        async Task<Boolean> predicate(List<Guid> ids, CancellationToken cancellationToken) {
            IEnumerable<VariantOptionId> optionIds = ids.Select(id => VariantOptionId.Create(id));
            return await variantRepository.VariantOptionsExistsAsync(optionIds, cancellationToken);
        }
        return ruleBuilder.MustAsync(predicate);
    }
}