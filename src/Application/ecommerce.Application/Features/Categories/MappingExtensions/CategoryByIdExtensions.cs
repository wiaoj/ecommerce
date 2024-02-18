using ecommerce.Application.Features.Categories.Queries.CategoryById;
using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Application.Features.Categories.MappingExtensions;
internal static class CategoryByIdExtensions {
    public static CategoryByIdResult ToCategoryByIdResult(this CategoryAggregate category, IEnumerable<CategoryAggregate> subcategories) {
        return new CategoryByIdResult(
            category.Id,
            category.ParentId,
            category.Name.Value,
            subcategories.ToCategoryByIdChildren()
        );
    }

    private static List<CategoryByIdResult.CategoryByIdChild> ToCategoryByIdChildren(this IEnumerable<CategoryAggregate> subcategories) {
        return subcategories.Select(subcategory => new CategoryByIdResult.CategoryByIdChild(subcategory.Id, subcategory.Name)).ToList();
    }
}