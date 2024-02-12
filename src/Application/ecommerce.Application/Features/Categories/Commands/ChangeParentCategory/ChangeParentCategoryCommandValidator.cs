using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
public sealed class ChangeParentCategoryCommandValidator : AbstractValidator<ChangeParentCategoryCommand> {
    public ChangeParentCategoryCommandValidator(ICategoryRepository categoryRepository) {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.ParentCategoryId)
            .NotNull()
            .NotEmpty()
            .CategoryExist(categoryRepository);
    }
}