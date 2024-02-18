using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.ValidatorExtensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
public sealed partial class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand> {
    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory) {
        RuleFor(x => x.Name)
            .CategoryNameMustBeValid()
            .CategoryNameMustBeUnique(categoryRepository, categoryFactory);

        RuleFor(x => x.ParentCategoryId)
            .CategoryExistsIfParentIdNotNull(categoryRepository, categoryFactory)
            .When(x => x.ParentCategoryId is not null);
    }
}