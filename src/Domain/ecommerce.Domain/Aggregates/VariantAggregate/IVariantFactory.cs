using ecommerce.Domain.Aggregates.VariantAggregate.Entities;

namespace ecommerce.Domain.Aggregates.VariantAggregate;
public interface IVariantFactory {
    VariantAggregate Create(Guid categoryId, String name);
    VariantOptionEntity CreateOption(Guid variantId, String value);
}