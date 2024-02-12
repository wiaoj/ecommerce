using MediatR;

namespace ecommerce.Application.Features.Variants.Queries.Variants;
public sealed record VariantsQuery : IRequest<VariantsResponse>;