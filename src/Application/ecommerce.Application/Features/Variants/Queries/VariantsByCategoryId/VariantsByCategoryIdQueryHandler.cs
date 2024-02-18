using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate;
using MediatR;

namespace ecommerce.Application.Features.Variants.Queries.VariantsByCategoryId;
internal sealed class VariantsByCategoryIdQueryHandler : IRequestHandler<VariantsByCategoryIdQuery, IEnumerable<VariantsByCategoryIdResponse>> {
    private readonly IVariantRepository variantRepository;

    public VariantsByCategoryIdQueryHandler(IVariantRepository variantRepository) {
        this.variantRepository = variantRepository;
    }

    public async Task<IEnumerable<VariantsByCategoryIdResponse>> Handle(VariantsByCategoryIdQuery request,
                                                                        CancellationToken cancellationToken) {
        IEnumerable<VariantAggregate> variants =
            await this.variantRepository.FindByCategoryId(CategoryId.Create(request.CategoryId), cancellationToken);
        return variants.Select(variant
            => new VariantsByCategoryIdResponse(variant.Id.Value,
                                                variant.Name,
                                                variant.Options.Select(option
                                                    => new VariantsByCategoryIdOptions(option.Id.Value,
                                                                                       option.Value.Value)).ToList()))
            .ToList();
    }
}