using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Products.Commands.CreateProduct;
public sealed record CreateProductCommand(
    Guid CategoryId,
    String Name,
    String Description,
    IEnumerable<CreateProductItemCommand> Items) : IRequest<CreateProductCommandResponse>, IHasTransaction, IHasEvent;

public sealed record CreateProductItemCommand(String Sku, Int32 Stock, Decimal Price, List<Guid> OptionIds);