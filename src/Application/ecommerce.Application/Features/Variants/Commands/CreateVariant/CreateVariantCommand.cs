using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Variants.Commands.CreateVariant;
public sealed record CreateVariantCommand(Guid CategoryId, String Name) : IRequest<Unit>, IHasTransaction, IHasEvent;