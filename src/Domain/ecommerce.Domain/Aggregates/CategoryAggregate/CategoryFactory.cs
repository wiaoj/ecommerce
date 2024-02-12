using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.CategoryAggregate;
public sealed class CategoryFactory : ICategoryFactory
{
    public CategoryAggregate Create(Guid? parentCategoryId, string name)
    {
        CategoryAggregate category = new(CategoryId.Create(parentCategoryId),
                                         CategoryId.CreateUnique,
                                         CategoryName.Create(name),
                                         []);
        category.RaiseDomainEvent(new CategoryCreatedDomainEvent(category));
        return category;
    }
}