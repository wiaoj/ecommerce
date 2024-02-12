using MediatR;

namespace ecommerce.Application.Features.Variants.Queries.VariantsByCategoryId;
public record VariantsByCategoryIdQuery(Guid CategoryId) : IRequest<IEnumerable<VariantsByCategoryIdResponse>>;