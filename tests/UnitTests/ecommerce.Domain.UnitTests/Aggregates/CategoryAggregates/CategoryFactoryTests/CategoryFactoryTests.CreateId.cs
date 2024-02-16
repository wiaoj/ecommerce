using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryFactoryTests;
public partial class CategoryFactoryTests {
    [Fact]
    public void CreateId_WithNonEmptyGuid_ShouldCreateValidCategoryId() {
        // Arrange
        Guid idValue = Guid.NewGuid();

        // Act
        CategoryId categoryId = this.factory.CreateId(idValue);

        // Assert
        categoryId.Should().NotBeNull();
        categoryId.Value.Should().Be(idValue);
    }

    [Fact]
    public void CreateId_WithNonNullNullableGuid_ShouldCreateValidCategoryId() {
        // Arrange
        Guid? idValue = Guid.NewGuid();

        // Act
        CategoryId? categoryId = this.factory.CreateId(idValue);

        // Assert
        categoryId.Should().NotBeNull();
        categoryId!.Value.Should().Be(idValue.Value);
    }

    [Fact]
    public void CreateId_WithNull_ReturnsNull() {
        // Arrange
        Guid? guid = null;

        // Act
        CategoryId? categoryId = this.factory.CreateId(guid);

        // Assert
        categoryId.Should().BeNull();
    }
}