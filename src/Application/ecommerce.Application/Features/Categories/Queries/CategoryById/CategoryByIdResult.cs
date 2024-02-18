using ecommerce.Domain.Aggregates.CategoryAggregate;
using static ecommerce.Application.Features.Categories.Queries.CategoryById.CategoryByIdResult;

namespace ecommerce.Application.Features.Categories.Queries.CategoryById;
public sealed record CategoryByIdResult(Guid Id, Guid? ParentId, String Name, ICollection<CategoryByIdChild> Subcategories) {
    public sealed record CategoryByIdChild(Guid Id, String Name);
}