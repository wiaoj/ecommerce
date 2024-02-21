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
    [InlineData("Invalid#Description")]
    [InlineData("Another@Invalid")]
    public void CreateProductDescription_WhenGivenInvalidStringOrCharacters_ShouldThrowException(String? description) {
        // Act & Assert
        Action act = () => ProductTestFactory.CreateProductDescription(description!);

        // Assert
        act.Should()
           .ThrowExactly<ProductDescriptionContainsInvalidCharactersException>()
           .WithMessage(ProductConstants.ErrorMessages.ProductDescriptionContainsInvalidCharacters);
    }

    [Fact]
    public void CreateProductDescription_WhenGivenValidString_ShouldCreateAndValueNotBeNullOrWhiteSpace() {
        // Arrange & Act
        ProductDescription description = ProductTestFactory.CreateProductDescription();

        // Assert
        description.Should().NotBeNull();
        description.Value.Should().NotBeNullOrWhiteSpace();
    }
}