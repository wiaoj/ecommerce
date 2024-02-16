using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryAggregateTests;
public partial class CategoryAggregateTests {
    [Fact]
    public void Delete_ShouldRaise_CategoryDeletedDomainEvent() {
        // Arrange
        CategoryAggregate category = CategoryTestFactory.CreateCategory();

        // Act
        category.Delete();

        // Assert
        category.DomainEvents.Should().ContainSingle();
        category.DomainEvents[0].Should().BeOfType<CategoryDeletedDomainEvent>();
        CategoryDeletedDomainEvent domainEvent = category.DomainEvents.OfType<CategoryDeletedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.Category.Should().Be(category);
    }
}