namespace ecommerce.Application.Features.Products.Commands.CreateProduct;
public sealed record CreateProductCommandResponse(Guid Id,
                                                  Guid CategoryId,
                                                  String Name,
                                                  String Description,
                                                  IEnumerable<CreateProductItemCommandResponse> Items);
public sealed record CreateProductItemCommandResponse(Guid Id, String Sku, Decimal Price, IEnumerable<Guid> OptionIds);