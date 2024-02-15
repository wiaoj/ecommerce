using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.CategoryAggregates.CategoryFactoryTests;
public partial class CategoryFactoryTests {
    private readonly ICategoryFactory factory;

    public CategoryFactoryTests() {
        this.factory = new CategoryFactory();
    }

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

    [Theory]
    [InlineData("   Category Name", "Category Name")]
    [InlineData("Category Name  ", "Category Name")]
    [InlineData("   Category Name   ", "Category Name")]
    public void CreateCategoryName_WithTrimmedString_ReturnsTrimmedCategoryName(String name, String expected) {
        // Act
        CategoryName categoryName = this.factory.CreateCategoryName(name);

        // Assert
        categoryName.Should().NotBeNull();
        categoryName.Value.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateCategoryName_WithInvalidString_ThrowsInvalidCategoryNameException(String? name) {
        // Act & Assert
        this.factory.Invoking(x => x.CreateCategoryName(name!)).Should().ThrowExactly<InvalidCategoryNameException>();
    }
}