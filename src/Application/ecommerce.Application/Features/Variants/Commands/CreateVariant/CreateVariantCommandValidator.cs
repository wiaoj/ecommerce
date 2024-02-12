using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators;
using ecommerce.Application.Validators.VariantValidators; 
using FluentValidation;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariant;
public sealed class CreateVariantCommandValidator : AbstractValidator<CreateVariantCommand> {
    public CreateVariantCommandValidator(IVariantRepository variantRepository, ICategoryRepository categoryRepository) {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .CategoryExist(categoryRepository);

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .VariantNotExist(variantRepository);
    }
}