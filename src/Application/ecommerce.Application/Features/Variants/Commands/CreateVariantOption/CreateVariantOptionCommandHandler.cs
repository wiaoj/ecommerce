using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate.Entities;
using MediatR;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariantOption;
internal sealed class CreateVariantOptionCommandHandler : IRequestHandler<CreateVariantOptionCommand, Unit> {
    private readonly IVariantRepository variantRepository;
    private readonly IVariantFactory variantFactory;

    public CreateVariantOptionCommandHandler(IVariantRepository variantRepository, IVariantFactory variantFactory) {
        this.variantRepository = variantRepository;
        this.variantFactory = variantFactory;
    }

    public async Task<Unit> Handle(CreateVariantOptionCommand request, CancellationToken cancellationToken) {
        VariantAggregate variant = await this.variantRepository.FindByIdAsync(request.VariantId, cancellationToken)
            ?? throw new Exception("Variant not found");

        VariantOptionEntity variantOptionEntity = this.variantFactory.CreateOption(request.VariantId, request.Value);
        variant.AddOption(variantOptionEntity);
        await this.variantRepository.UpdateAsync(variant, cancellationToken);
        return Unit.Value;
    }
}