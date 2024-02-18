using ecommerce.Application.Features.Categories.ValidatorExtensions;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.Commands.DeleteCategory;
public sealed partial class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand> {
    public DeleteCategoryCommandValidator() {
        RuleFor(x => x.Id).CategoryIdMustBeValid();
    }
}