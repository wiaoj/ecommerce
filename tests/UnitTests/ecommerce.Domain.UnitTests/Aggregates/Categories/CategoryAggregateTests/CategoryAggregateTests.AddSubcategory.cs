using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void AddSubcategory_WhenValidCategoryId_AddsSubcategorySuccessfully() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId subcategoryId = CategoryTestFactory.CreateSubcategoryId();

        // Act
        category.AddSubcategory(subcategoryId);

        // Assert
        category.SubcategoryIds.Should().Contain(subcategoryId);
        category.DomainEvents.Should().ContainSingle();
    }

    [Fact]
    public void AddSubcategory_WhenCategoryIsSelfReferencing_ThrowsSelfReferencingCategoryException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();

        // Act & Assert
        category.Invoking(x => x.AddSubcategory(category.Id))
                .Should()
                .ThrowExactly<SelfReferencingCategoryException>();
    }

    [Fact]
    public void AddSubcategory_WhenSubcategoryAlreadyExists_ThrowsSubcategoryAlreadyExistsException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId subcategoryId = CategoryTestFactory.CreateSubcategoryId();
        category.AddSubcategory(subcategoryId);

        // Act & Assert
        category.Invoking(x => x.AddSubcategory(subcategoryId))
                .Should()
                .Throw<SubcategoryAlreadyExistsException>();
    }

    [Fact]
    public void AddSubcategory_WhenValidCategoryId_AddsToSubcategoryIds() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId subcategoryId = CategoryTestFactory.CreateSubcategoryId();

        // Act
        category.AddSubcategory(subcategoryId);

        // Assert
        category.SubcategoryIds.Should().Contain(subcategoryId);
    }

    [Fact]
    public void AddSubcategory_WhenValidCategoryId_RaisesSubcategoryAddedDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId subcategoryId = CategoryTestFactory.CreateSubcategoryId();

        // Act
        category.AddSubcategory(subcategoryId);

        // Assert
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<SubcategoryAddedDomainEvent>();
        SubcategoryAddedDomainEvent domainEvent = category.DomainEvents.OfType<SubcategoryAddedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.CategoryId.Should().Be(category.Id);
        domainEvent.SubcategoryId.Should().Be(subcategoryId);
    }
}