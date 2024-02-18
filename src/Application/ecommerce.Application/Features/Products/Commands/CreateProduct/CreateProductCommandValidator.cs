using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.ValidatorExtensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentValidation;

namespace ecommerce.Application.Features.Products.Commands.CreateProduct;
public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
    public CreateProductCommandValidator(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory) {
        RuleFor(x => x.CategoryId)
            .CategoryIdMustBeValid()
            .CategoryMustExist(categoryRepository, categoryFactory);

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