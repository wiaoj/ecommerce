using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.VariantAggregate.Entities;
public sealed class VariantOptionEntity : Entity<VariantOptionId>
{
    public VariantId VariantId { get; private set; }
    public VariantOptionValue Value { get; private set; }

    private VariantOptionEntity() { }
    internal VariantOptionEntity(VariantOptionId id,
                                 VariantId variantId,
                                 VariantOptionValue value) : base(id)
    {
        VariantId = variantId;
        Value = value;
    }
}