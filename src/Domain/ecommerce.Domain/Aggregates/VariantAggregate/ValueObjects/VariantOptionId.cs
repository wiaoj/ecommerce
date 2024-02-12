using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
public sealed record VariantOptionId : EntityId<Guid>
{
    private VariantOptionId() { }
    private VariantOptionId(Guid value) : base(value) { }

    public static VariantOptionId CreateUnique => new(Guid.NewGuid());
    public static VariantOptionId Create(Guid value)
    {
        return new(value);
    }
}