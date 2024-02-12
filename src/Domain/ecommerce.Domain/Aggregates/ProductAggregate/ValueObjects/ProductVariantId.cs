using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
public sealed record ProductVariantId : EntityId<Guid>
{
    public ProductVariantId(Guid value) : base(value) { }

    public static ProductVariantId CreateUnique => new(Guid.NewGuid());
    public static ProductVariantId Create(Guid value)
    {
        return new(value);
    }
}