using ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Products;

namespace ecommerce.Domain.UnitTests.Aggregates.Products.ProductFactoryTests;
public partial class ProductFactoryTests {
    [Fact]
    public void CreateProductDescription_WithValidString_ReturnsCorrectInstance() {
        // Arrange
        String validString = ProductTestFactory.CreateProductDescription().Value;

        // Act
        ProductDescription result = this.factory.CreateProductDescription(validString);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ProductDescription>();
        result.Value.Should().Be(validString);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateProductDescription_WithInvalidString_ThrowsProductDescriptionContainsInvalidCharactersException(String? invalidString) {
        // Act & Assert
        this.factory.Invoking(x => x.CreateProductDescription(invalidString!))
            .Should()
            .ThrowExactly<ProductDescriptionContainsInvalidCharactersException>();
    }
}