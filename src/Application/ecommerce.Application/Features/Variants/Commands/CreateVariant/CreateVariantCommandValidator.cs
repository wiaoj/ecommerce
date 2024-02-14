using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Validators.CategoryValidators;
using ecommerce.Application.Validators.VariantValidators;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using FluentValidation;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariant;
public sealed class CreateVariantCommandValidator : AbstractValidator<CreateVariantCommand> {
    public CreateVariantCommandValidator(IVariantRepository variantRepository,
                                         ICategoryRepository categoryRepository,
                                         ICategoryFactory categoryFactory) {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .CategoryExist(categoryRepository, categoryFactory);

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .VariantNotExist(variantRepository);
    }
}