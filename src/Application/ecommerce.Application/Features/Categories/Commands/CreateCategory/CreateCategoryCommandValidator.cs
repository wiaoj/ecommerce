using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
public sealed partial class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand> {
    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository) {
        RuleFor(x => x.Name)
            .NotNull()
            .MaximumLength(50)
            .Matches(CategoryNameRegex())
            .WithMessage("Category name format: ^[A-Za-zçÇğĞıİöÖşŞüÜ ]+$")
            .CategoryExist(categoryRepository)
            .WithMessage("Category already exists.");

        RuleFor(x => x.ParentCategoryId)
            .CategoryExistIfIdNotNull(categoryRepository)
            .When(x => x.ParentCategoryId is not null)
            .WithMessage("Parent Category does not exist.");
    }

    [GeneratedRegex("^[A-Za-zçÇğĞıİöÖşŞüÜ ]+$")]
    private static partial Regex CategoryNameRegex();
}