using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Products.Commands.CreateProductVariant;
public sealed record CreateProductVariantCommand(Guid ProductId,
                                                 Int32 Stock,
                                                 Decimal Price,
                                                 List<Guid> Options) : IRequest<Unit>, IHasTransaction, IHasEvent;