using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;
using ecommerce.UnitTests.Common.Products;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void AddProduct_WhenNewProductId_ShouldAdd() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateValidCategoryAggregate();
        ProductId productId = ProductTestFactory.CreateValidProductId();

        // Act
        category.AddProduct(productId);

        // Assert
        category.ProductIds.Should().Contain(productId);
    }

    [Fact]
    public void AddProduct_WhenProductAlreadyExists_ThrowsProductAlreadyInCategoryException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateValidCategoryAggregateWithProducts();
        ProductId productId = category.ProductIds.First();

        // Act & Assert
        category.Invoking(x => x.AddProduct(productId))
                .Should()
                .ThrowExactly<ProductAlreadyInCategoryException>();
    }

    [Fact]
    public void AddProduct_WhenProductAdded_RaisesProductAddedToCategoryDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateValidCategoryAggregate();
        ProductId existingProductId = ProductTestFactory.CreateValidProductId();

        // Act
        category.AddProduct(existingProductId);

        // Assert
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<ProductAddedToCategoryDomainEvent>();
        ProductAddedToCategoryDomainEvent domainEvent = category.DomainEvents.OfType<ProductAddedToCategoryDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.ProductId.Should().Be(existingProductId);
        domainEvent.CategoryId.Should().Be(category.Id);
    }
}