using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
public sealed class ChangeParentCategoryCommandValidator : AbstractValidator<ChangeParentCategoryCommand> {
    public ChangeParentCategoryCommandValidator(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory) {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.ParentCategoryId)
            .NotNull()
            .NotEmpty()
            .CategoryExist(categoryRepository, categoryFactory);
    }
}