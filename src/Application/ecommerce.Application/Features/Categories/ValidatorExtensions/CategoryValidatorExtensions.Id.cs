using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Extensions;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.ValidatorExtensions;
internal static partial class CategoryValidatorExtensions {
    public static IRuleBuilderOptions<T, Guid?> CategoryExistsIfParentIdNotNull<T>(this IRuleBuilder<T, Guid?> ruleBuilder,
                                                                      ICategoryRepository categoryRepository,
                                                                      ICategoryFactory categoryFactory) {
        return ruleBuilder.MustAsync(async (id, cancellationToken) =>
             id.HasValue && await categoryRepository.ExistsAsync(categoryFactory.CreateId(id.Value), cancellationToken))
            .WithMessage(CategoryApplicationConstants.ValidationMessages.ParentCategoryDoesNotExist);
    }

    public static IRuleBuilderOptions<T, Guid> CategoryIdMustBeValid<T>(this IRuleBuilder<T, Guid> ruleBuilder) {
        return ruleBuilder.NotNull()
                          .NotEmpty()
                          .WithMessage(CategoryApplicationConstants.ValidationMessages.Id.CannotBeEmpty);
    }

    public static IRuleBuilderOptions<T, Guid> CategoryParentIdMustBeValid<T>(this IRuleBuilder<T, Guid> ruleBuilder) {
        return ruleBuilder.NotNull()
                          .NotEmpty()
                          .WithMessage(CategoryApplicationConstants.ValidationMessages.Id.ParentIdCannotBeEmpty);
    }
}