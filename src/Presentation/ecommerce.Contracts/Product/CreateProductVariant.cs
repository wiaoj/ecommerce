using ecommerce.Application.Features.Products.Commands.CreateProductVariant;

namespace ecommerce.Contracts;
public partial class External {
    public sealed record CreateProductVariantRequest(Guid ProductId,
                                                     Int32 Stock,
                                                     Decimal Price,
                                                     List<Guid> Options) {

        public CreateProductVariantCommand ToCommand() {
            return new(this.ProductId, this.Stock, this.Price, this.Options);
        }
    }
}