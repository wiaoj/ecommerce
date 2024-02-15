using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.CategoryAggregate;
public sealed class CategoryFactory : ICategoryFactory {
    public CategoryAggregate Create(CategoryName name) {
        return Create(null, name);
    }

    public CategoryAggregate Create(CategoryId? parentCategoryId, CategoryName name) {
        CategoryAggregate category = new(parentCategoryId,
                                         CategoryId.CreateUnique,
                                         name,
                                         [],
                                         []);
        category.RaiseDomainEvent(new CategoryCreatedDomainEvent(category));
        return category;
    }

    public CategoryId CreateId(Guid value) {
        return new(value);
    }

    public CategoryId? CreateId(Guid? value) {
        return value is null ? null : new(Guid.Parse(value.ToString()!));
    }

    public CategoryName CreateCategoryName(String name) {
        return new(name);
    }
}