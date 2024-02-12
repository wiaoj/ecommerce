using ecommerce.Domain.Aggregates.CategoryAggregate;

namespace ecommerce.Application.Features.Categories.Queries.Categories;
public sealed record CategoriesResult(Guid Id, String Name, Guid? ParentId, IEnumerable<Guid> ChildIds) {
    public static CategoriesResult FromCategoryAggregate(CategoryAggregate category) {
        return new(category.Id,
                   category.Name,
                   category.ParentId,
                   category.SubcategoryIds.Select(id => id.Value));
    }
}