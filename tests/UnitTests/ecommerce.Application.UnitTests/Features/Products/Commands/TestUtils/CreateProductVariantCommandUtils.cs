using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Application.Features.Products.Commands.CreateProductVariant;

namespace ecommerce.Application.UnitTests.Features.Products.Commands.TestUtils;
public static class CreateProductVariantCommandUtils
{
    public static CreateProductVariantCommand CreateVariantCommand()
    {
        return new(Product.Id, Product.Stock, Product.Price, Product.OptionIds);
    }
}