using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Domain.UnitTests.Aggregates.Categories.ValueObjectTests;
public partial class ValueObjectTests {
    [Theory]
    [InlineData("   Category Name", "Category Name")]
    [InlineData("Category Name  ", "Category Name")]
    [InlineData("   Category Name   ", "Category Name")]
    public void CreateCategoryName_WhenGivenStringWithLeadingOrTrailingSpaces_ShouldTrimAndReturnCorrectCategoryName(String name,
                                                                                                                     String expected) {
        // Act
        CategoryName categoryName = CategoryTestFactory.CreateCategoryName(name);

        // Assert
        categoryName.Should().NotBeNull();
        categoryName.Value.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("Invalid#Name")]
    [InlineData("Another@Invalid")]
    public void CreateCategoryName_WhenGivenInvalidStringOrCharacters_ShouldThrowInvalidCategoryNameException(String? name) {
        // Act & Assert
        Action act = () => CategoryTestFactory.CreateCategoryName(name!);

        // Assert
        act.Should()
           .ThrowExactly<InvalidCategoryNameException>()
           .WithMessage(CategoryConstants.ErrorMessages.InvalidCategoryName.Format(name));
    }

    [Fact]
    public void CreateCategoryName_WhenGivenValidString_ShouldCreateCategoryNameWithExpectedValue() {
        // Arrange
        String validName = "Valid Category Name";

        // Act
        CategoryName categoryName = CategoryTestFactory.CreateCategoryName(validName);

        // Assert
        categoryName.Should().NotBeNull();
        categoryName.Value.Should().Be(validName);
    }
}