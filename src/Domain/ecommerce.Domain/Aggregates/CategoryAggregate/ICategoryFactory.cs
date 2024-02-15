using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.CategoryAggregate;
public interface ICategoryFactory {
    CategoryAggregate Create(CategoryName name);
    CategoryAggregate Create(CategoryId? parentCategoryId, CategoryName name);
    CategoryId CreateId(Guid value);
    CategoryId? CreateId(Guid? value);
    CategoryName CreateCategoryName(String name);
}