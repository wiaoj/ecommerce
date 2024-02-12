using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Contracts.Common;

namespace ecommerce.Contracts;
public partial class External {
    public sealed record CreateProductRequest(
        Guid CategoryId,
        String Name,
        String Description,
        IEnumerable<CreateProductRequest.CreateProductItem> Variants) : IExternalRequest<CreateProductCommand> {
        public sealed record CreateProductItem(String Sku, Int32 Stock, Decimal Price, List<Guid> OptionIds) {
            public CreateProductItemCommand ToItemCommand() {
                return new CreateProductItemCommand(this.Sku,
                                                    this.Stock,
                                                    this.Price,
                                                    this.OptionIds);
            }
        }

        public CreateProductCommand ToCommand() {
            return new(this.CategoryId,
                       this.Name,
                       this.Description,
                       this.Variants.Select(variant => variant.ToItemCommand()));
        }
    }

    public sealed record CreatedProductResponse(Guid Id, Guid CategoryId);
}