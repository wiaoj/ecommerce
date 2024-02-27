using ecommerce.Domain.Aggregates.ProductAggregate.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Products;

namespace ecommerce.Domain.UnitTests.Aggregates.Products.ValueObjectTests;
public partial class ValueObjectTests {
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("Invalid#Name")]
    [InlineData("Another@Invalid")]
    public void CreateProductName_WhenGivenInvalidStringOrCharacters_ShouldThrowException(String? name) {
        // Act & Assert
        Action act = () => ProductTestFactory.CreateProductName(name!);

        // Assert
        act.Should()
           .ThrowExactly<ProductNameContainsInvalidCharactersException>()
           .WithMessage(ProductConstants.ErrorMessages.ProductNameContainsInvalidCharacters);
    }

    [Fact]
    public void CreateProductName_WhenGivenValidString_ShouldCreateAndValueNotBeNullOrWhiteSpace() {
        // Arrange & Act
        ProductName name = ProductTestFactory.CreateProductName();

        // Assert
        name.Should().NotBeNull();
        name.Value.Should().NotBeNullOrWhiteSpace();
    }
}