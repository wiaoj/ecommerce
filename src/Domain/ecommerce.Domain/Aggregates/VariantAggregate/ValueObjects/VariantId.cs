using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
public sealed record VariantId : AggregateRootId<Guid>
{
    private VariantId() { }
    private VariantId(Guid value) : base(value) { }

    public static VariantId CreateUnique => new(Guid.NewGuid());
    public static VariantId Create(Guid value) => new(value);

    public static implicit operator VariantId(Guid value) => new(value);
}