using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate.Entities;
using ecommerce.Domain.Aggregates.VariantAggregate.Events;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.VariantAggregate;
public sealed class VariantFactory : IVariantFactory
{
    public VariantAggregate Create(Guid categoryId, string name)
    {
        VariantAggregate variant = new(VariantId.CreateUnique, CategoryId.Create(categoryId), name, []);
        variant.RaiseDomainEvent(new VariantCreatedDomainEvent(variant));
        return variant;
    }

    public VariantOptionEntity CreateOption(Guid variantId, string value)
    {
        return new(VariantOptionId.CreateUnique, variantId, VariantOptionValue.Create(value));
    }
}