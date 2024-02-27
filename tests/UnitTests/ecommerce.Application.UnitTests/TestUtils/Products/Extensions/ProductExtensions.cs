using ecommerce.Application.Features.Products.Commands.CreateProduct;
using FluentAssertions;

namespace ecommerce.Application.UnitTests.TestUtils.Products.Extensions;
public static partial class ProductExtensions {
    public static void ValidateCreatedFrom(this CreateProductCommandResult response, CreateProductCommand command) {
        response.Id.Should().NotBe(Guid.Empty);
        response.CategoryId.Should().Be(command.CategoryId);
        response.Name.Should().Be(command.Name);
        response.Description.Should().Be(command.Description);
    }
}