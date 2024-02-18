using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Extensions;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.ValidatorExtensions;
internal static partial class CategoryValidatorExtensions {
    public static IRuleBuilderOptions<T, String> CategoryNameMustBeUnique<T>(this IRuleBuilder<T, String> ruleBuilder,
                                                                      ICategoryRepository categoryRepository,
                                                                      ICategoryFactory categoryFactory) {
        return ruleBuilder.MustAsync(async (name, cancellationToken) =>
        await categoryRepository.ExistsByNameAsync(categoryFactory.CreateCategoryName(name), cancellationToken).IsFalse())
           .WithMessage(CategoryApplicationConstants.ValidationMessages.CategoryAlreadyExists);
    }

    public static IRuleBuilderOptions<T, String> CategoryNameMustBeValid<T>(this IRuleBuilder<T, String> ruleBuilder) {
        return ruleBuilder.NotNull()
                          .NotEmpty()
                          .WithMessage(CategoryApplicationConstants.ValidationMessages.CategoryName.CannotBeEmpty)
                          .MaximumLength(CategoryConstants.Rules.Name.MaximumLength)
                          .WithMessage(CategoryApplicationConstants.ValidationMessages.CategoryName.MaxLength);
    }
}