using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.ValidatorExtensions;
internal static partial class CategoryValidatorExtensions {
    public static IRuleBuilderOptions<T, Guid> CategoryMustExist<T>(this IRuleBuilder<T, Guid> ruleBuilder,
                                                                      ICategoryRepository categoryRepository,
                                                                      ICategoryFactory categoryFactory) {
        return ruleBuilder.MustAsync(async (id, cancellationToken) =>
        await categoryRepository.ExistsAsync(categoryFactory.CreateId(id)!, cancellationToken))
            .WithMessage(CategoryApplicationConstants.ValidationMessages.CategoryNotFound);
    }
}