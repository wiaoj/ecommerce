using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using FluentAssertions;

namespace ecommerce.Application.UnitTests.TestUtils.Categories.Extensions;
public static partial class CategoryExtensions {
    public static void ValidateCreatedFrom(this CreateCategoryCommandResult response, CreateCategoryCommand command) {
        response.ParentId.Should().Be(command.ParentCategoryId);
        response.Name.Should().Be(command.Name); 
    }
}