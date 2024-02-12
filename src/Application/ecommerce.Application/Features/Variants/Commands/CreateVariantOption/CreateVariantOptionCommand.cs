using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariantOption;
public sealed record CreateVariantOptionCommand(Guid CategoryId, Guid VariantId, String Value) : IRequest<Unit>, IHasTransaction, IHasEvent;