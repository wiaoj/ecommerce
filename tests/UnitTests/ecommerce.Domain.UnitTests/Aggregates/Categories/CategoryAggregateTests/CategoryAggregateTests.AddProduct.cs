using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;
using ecommerce.UnitTests.Common.Products;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void AddProduct_WhenNewProductId_ShouldAdd() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        ProductId productId = ProductTestFactory.CreateProductId();

        // Act
        category.AddProduct(productId);

        // Assert
        category.ProductIds.Should().Contain(productId);
    }

    [Fact]
    public void AddProduct_WhenProductAlreadyExists_ThrowsProductAlreadyInCategoryException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategoryWithProducts();
        ProductId productId = category.ProductIds.First();

        // Act & Assert
        category.Invoking(x => x.AddProduct(productId))
                .Should()
                .ThrowExactly<ProductAlreadyInCategoryException>();
    }

    [Fact]
    public void AddProduct_WhenProductAdded_RaisesProductAddedToCategoryDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        ProductId existingProductId = ProductTestFactory.CreateProductId();

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