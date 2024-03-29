﻿using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void SetParentCategory_WhenCategoryIsSetAsOwnParent_ThrowsCategoryCannotBeOwnParentException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId categoryId = category.Id;

        // Act & Assert
        category.Invoking(x => x.SetParentCategory(categoryId))
                .Should()
                .ThrowExactly<CategoryCannotBeOwnParentException>();
    }

    [Fact]
    public void SetParentCategory_WhenParentCategoryIdIsAlreadySet_ThrowsParentCategoryAlreadySetException() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory(true);
        CategoryId parentCategoryId = category.ParentId!;

        // Act & Assert
        category.Invoking(x => x.SetParentCategory(parentCategoryId))
                .Should()
                .ThrowExactly<ParentCategoryAlreadySetException>();
    }

    [Fact]
    public void SetParentCategory_WhenParentCategoryIdIsValid_SetsParentCategorySuccessfully() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();
        CategoryId parentCategoryId = CategoryTestFactory.CreateParentCategoryId();

        // Act
        category.SetParentCategory(parentCategoryId);

        // Assert
        category.ParentId.Should().Be(parentCategoryId);
        category.DomainEvents.Should().ContainSingle(x => x is ParentCategoryChangedDomainEvent);
    }

    [Fact]
    public void SetParentCategory_WhenParentCategoryIsChanged_RaisesParentCategoryRemovedAndChangedDomainEvents() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory(true);
        CategoryId oldParentCategoryId = category.ParentId!;
        CategoryId newParentCategoryId = CategoryTestFactory.CreateParentCategoryId();

        // Act
        category.SetParentCategory(newParentCategoryId);

        // Assert
        category.ParentId.Should().Be(newParentCategoryId);
        category.DomainEvents.Should().HaveCount(2);
        category.DomainEvents.Should().ContainItemsAssignableTo<ParentCategoryRemovedDomainEvent>();
        category.DomainEvents.Should().ContainItemsAssignableTo<ParentCategoryChangedDomainEvent>();

        ParentCategoryRemovedDomainEvent removedEvent = category.DomainEvents
            .OfType<ParentCategoryRemovedDomainEvent>()
            .First();
        removedEvent.Should().NotBeNull();
        removedEvent.CategoryId.Should().Be(category.Id);
        removedEvent.ParentId.Should().Be(oldParentCategoryId);

        ParentCategoryChangedDomainEvent changedEvent = category.DomainEvents
            .OfType<ParentCategoryChangedDomainEvent>()
            .First();
        changedEvent.Should().NotBeNull();
        changedEvent.CategoryId.Should().Be(category.Id);
        changedEvent.ParentId.Should().Be(newParentCategoryId);
    }
}