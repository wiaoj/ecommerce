using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.Extensions;
public static partial class CategoryExtensions {
    public static void VerifyCategoryResultFromCategory(this CreateCategoryCommandResult categoryResult, CategoryAggregate category) {
        categoryResult.Should().NotBeNull();
        categoryResult.Id.Should().Be(category.Id);
        categoryResult.ParentId.Should().Be(category.ParentId);
        categoryResult.Name.Should().Be(category.Name.Value);
        categoryResult.Should().BeEquivalentTo(category, options => options.ExcludingMissingMembers());
    }
}