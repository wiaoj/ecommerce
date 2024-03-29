﻿using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void RemoveSubCategory_WhenSubCategoryIdDoesNotExist_ThrowsSubcategoryNotFoundException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId nonExistentCategoryId = CategoryTestFactory.CreateSubcategoryId();

        // Act & Assert
        category.Invoking(x => x.RemoveSubcategory(nonExistentCategoryId))
            .Should()
            .ThrowExactly<SubcategoryNotFoundException>();
    }

    [Fact]
    public void RemoveSubCategory_WhenExistingSubCategoryId_RemovesSubCategorySuccessfully() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory(1);
        CategoryId subcategoryId = category.SubcategoryIds.First();

        // Act
        category.RemoveSubcategory(subcategoryId);

        // Assert
        category.SubcategoryIds.Should().NotContain(subcategoryId);
    }

    [Fact]
    public void RemoveSubCategory_WhenValidCategoryId_RaisesSubcategoryRemovedDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory(1);
        CategoryId subcategoryId = category.SubcategoryIds.First();

        // Act
        category.RemoveSubcategory(subcategoryId);

        // Assert
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<SubcategoryRemovedDomainEvent>();
        SubcategoryRemovedDomainEvent domainEvent = category.DomainEvents.OfType<SubcategoryRemovedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.CategoryId.Should().Be(category.Id);
        domainEvent.SubcategoryId.Should().Be(subcategoryId);
    }
}