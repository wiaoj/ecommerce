using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Domain.UnitTests.Aggregates.Categories.CategoryFactoryTests;
public partial class CategoryFactoryTests {
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