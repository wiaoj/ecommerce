using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void RemoveParentCategory_WhenParentIdIsNotNull_RemovesParentCategorySuccessfully() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateValidCategoryAggregate(true);

        // Act
        category.RemoveParentCategory();

        // Assert
        category.ParentId.Should().BeNull();
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<ParentCategoryRemovedDomainEvent>();
    }

    [Fact]
    public void RemoveParentCategory_WhenValidCategoryId_RaisesParentCategoryRemovedDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateValidCategoryAggregate(true);
        CategoryId parentCategoryId = category.ParentId!;

        // Act
        category.RemoveParentCategory();

        // Assert
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<ParentCategoryRemovedDomainEvent>();
        ParentCategoryRemovedDomainEvent domainEvent = category.DomainEvents.OfType<ParentCategoryRemovedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.CategoryId.Should().Be(category.Id);
        domainEvent.ParentId.Should().Be(parentCategoryId);
    }

    [Fact]
    public void RemoveParentCategory_WhenParentIdIsNull_ThrowsParentCategoryNotSetException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateValidCategoryAggregate();

        // Act & Assert
        category.Invoking(x => x.RemoveParentCategory())
                .Should()
                .ThrowExactly<ParentCategoryNotSetException>();
    }
}