using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Application.Features.Products.Commands.CreateProductVariant;

namespace ecommerce.Application.UnitTests.Features.Products.Commands.TestUtils;
public static class CreateProductCommandUtils
{
    public static CreateProductCommand CreateCommand()
    {
        return new(Product.CategoryId, Product.Name, Product.Description, []);
    }
}