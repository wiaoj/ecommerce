using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.VariantValidators;
using FluentValidation;

namespace ecommerce.Application.Features.Products.Commands.CreateProductVariant;
public sealed class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand> {
    public CreateProductVariantCommandValidator(IVariantRepository variantRepository) {
        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.Options).OptionsExist(variantRepository);
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}