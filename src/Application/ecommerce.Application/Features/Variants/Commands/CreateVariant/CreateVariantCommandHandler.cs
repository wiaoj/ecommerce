using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.VariantAggregate;
using MediatR;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariant;
internal sealed class CreateVariantCommandHandler : IRequestHandler<CreateVariantCommand, Unit> {
    private readonly IVariantRepository variantRepository;
    private readonly IVariantFactory variantFactory;

    public CreateVariantCommandHandler(IVariantRepository variantRepository, IVariantFactory variantFactory) {
        this.variantRepository = variantRepository;
        this.variantFactory = variantFactory;
    }

    public async Task<Unit> Handle(CreateVariantCommand request, CancellationToken cancellationToken) {
        VariantAggregate variantAggregate = this.variantFactory.Create(request.CategoryId, request.Name);
        await this.variantRepository.CreateAsync(variantAggregate, cancellationToken);
        return Unit.Value;
    }
}