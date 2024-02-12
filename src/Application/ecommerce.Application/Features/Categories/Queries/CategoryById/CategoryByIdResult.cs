using ecommerce.Domain.Aggregates.CategoryAggregate;
using static ecommerce.Application.Features.Categories.Queries.CategoryById.CategoryByIdResult;

namespace ecommerce.Application.Features.Categories.Queries.CategoryById;
public sealed record CategoryByIdResult(Guid Id, Guid? ParentId, String Name, ICollection<CategoryByIdChild> SubCategories) {
    public sealed record CategoryByIdChild(Guid Id, String Name);

    public static CategoryByIdResult FromCategoryAggregate(CategoryAggregate category) {
        return new CategoryByIdResult(
            category.Id,
            category.ParentId,
            category.Name.Value,
            []
        );
    }

    internal void AddSubCategories(List<CategoryAggregate> subCategories) { 
        _ = subCategories.Aggregate(this.SubCategories, (list, child) => {
            list.Add(new(child.Id, child.Name.Value));
            return list;
        });
    }
}