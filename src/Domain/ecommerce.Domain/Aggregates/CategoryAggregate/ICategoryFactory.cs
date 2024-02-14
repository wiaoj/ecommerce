namespace ecommerce.Domain.Aggregates.CategoryAggregate;
public interface ICategoryFactory {
    CategoryAggregate Create(Guid? parentCategoryId, String name);
}