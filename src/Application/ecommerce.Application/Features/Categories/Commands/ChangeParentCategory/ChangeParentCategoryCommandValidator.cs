using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.ValidatorExtensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
public sealed class ChangeParentCategoryCommandValidator : AbstractValidator<ChangeParentCategoryCommand> {
    public ChangeParentCategoryCommandValidator(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory) {
        RuleFor(x => x.Id).CategoryIdMustBeValid();

        RuleFor(x => x.ParentCategoryId)
            .CategoryParentIdMustBeValid()
            .CategoryMustExist(categoryRepository, categoryFactory);
    }
}