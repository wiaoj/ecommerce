using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.Categories.CategoryFactoryTests;
public partial class CategoryFactoryTests {
    [Theory]
    [InlineData("Valid Category Name", "Valid Category Name")]
    [InlineData("CategoryName", "CategoryName")]
    public void CreateCategoryName_WhenGivenValidString_ShouldReturnCategoryNameWithExpectedValue(String name, String expected) {
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
    [InlineData("Invalid category name!")]
    [InlineData("Invalid category name2")]
    public void CreateCategoryName_WhenGivenInvalidString_ShouldThrowInvalidCategoryNameException(String? name) {
        // Act & Assert
        this.factory.Invoking(x => x.CreateCategoryName(name!)).Should().ThrowExactly<InvalidCategoryNameException>();
    }
}