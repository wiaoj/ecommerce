using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators;
using ecommerce.Application.Validators.VariantValidators;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using FluentValidation;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariantOption;
public sealed class CreateVariantOptionCommandValidator : AbstractValidator<CreateVariantOptionCommand> {
    public CreateVariantOptionCommandValidator(ICategoryRepository categoryRepository,
                                               IVariantRepository variantRepository,
                                               ICategoryFactory categoryFactory) {
        RuleFor(x => x.CategoryId)
            .CategoryExist(categoryRepository, categoryFactory);

        RuleFor(x => x.VariantId)
            .VariantExist(variantRepository);

        RuleFor(x => x.Value)
            .NotEmpty()
            .NotNull()
            .MustNotHaveExistingVariantOption(x => x.VariantId, variantRepository);
    }
}