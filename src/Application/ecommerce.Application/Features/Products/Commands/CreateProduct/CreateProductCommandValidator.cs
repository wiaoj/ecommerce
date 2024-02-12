using ecommerce.Application.Common.Constants;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators; 
using FluentValidation;

namespace ecommerce.Application.Features.Products.Commands.CreateProduct;
public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
    public CreateProductCommandValidator(ICategoryRepository categoryRepository) {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty()
            .CategoryExist(categoryRepository);

        RuleFor(x => x.Name)
            .NotNull()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(250)
            .When(x => x.Description is not null);

        //RuleFor(x => x.Price)
        //    .NotNull()
        //    .LessThanOrEqualTo(Decimal.MaxValue)
        //    .GreaterThanOrEqualTo(1M);
    }
}