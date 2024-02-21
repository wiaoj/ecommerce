using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;
using ecommerce.UnitTests.Common.Products;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void RemoveProduct_WhenProductIdIsValid_RemovesProductSuccessfully() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategoryWithProducts();
        ProductId productId = category.ProductIds.First();

        // Act
        category.RemoveProduct(productId);

        // Assert
        category.ProductIds.Should().NotContain(productId);
    }

    [Fact]
    public void RemoveProduct_WhenProductIdDoesNotExist_ThrowsProductNotInCategoryException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        ProductId nonExistentProductId = ProductTestFactory.CreateProductId();

        // Act & Assert
        category.Invoking(x => x.RemoveProduct(nonExistentProductId))
                .Should()
                .ThrowExactly<ProductNotInCategoryException>();
    }

    [Fact]
    public void RemoveProduct_WhenProductRemoved_RaisesProductRemovedFromCategoryDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategoryWithProducts();
        ProductId existingProductId = category.ProductIds.First();

        // Act
        category.RemoveProduct(existingProductId);

        // Assert
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<ProductRemovedFromCategoryDomainEvent>();
        ProductRemovedFromCategoryDomainEvent domainEvent = category.DomainEvents.OfType<ProductRemovedFromCategoryDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.ProductId.Should().Be(existingProductId);
        domainEvent.CategoryId.Should().Be(category.Id);
    }
}