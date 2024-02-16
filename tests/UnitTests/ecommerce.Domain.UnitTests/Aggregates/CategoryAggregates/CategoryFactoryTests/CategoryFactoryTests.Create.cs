using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryFactoryTests;
public partial class CategoryFactoryTests {
    [Fact]
    public void Create_WithName_CreatesCategoryWithoutParent() {
        // Arrange
        CategoryName name = CategoryTestFactory.CreateValidCategoryName();

        // Act
        CategoryAggregate category = this.factory.Create(name);

        // Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(name);
        category.ParentId.Should().BeNull();
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<CategoryCreatedDomainEvent>();
        CategoryCreatedDomainEvent domainEvent = category.DomainEvents.OfType<CategoryCreatedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.Category.Should().Be(category);
    }

    [Fact]
    public void Create_WithParentCategoryIdAndName_CreatesCategoryWithParent() {
        // Arrange
        CategoryId parentCategoryId = CategoryTestFactory.CreateValidParentCategoryId();
        CategoryName name = CategoryTestFactory.CreateValidCategoryName();

        // Act
        CategoryAggregate category = this.factory.Create(parentCategoryId, name);

        // Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(name);
        category.ParentId.Should().Be(category.ParentId);
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<CategoryCreatedDomainEvent>();
        CategoryCreatedDomainEvent domainEvent = category.DomainEvents.OfType<CategoryCreatedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.Category.Should().Be(category);
    }
}