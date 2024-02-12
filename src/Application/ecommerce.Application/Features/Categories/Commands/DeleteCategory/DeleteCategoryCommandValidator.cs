using ecommerce.Application.Common.Repositories;
using FluentValidation;

namespace ecommerce.Application.Features.Categories.Commands.DeleteCategory;
public sealed partial class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand> {
    public DeleteCategoryCommandValidator() {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();
    }
}