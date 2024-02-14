using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
public sealed record ProductId : AggregateRootId<Guid>
{
    private ProductId() { }
    internal ProductId(Guid value) : base(value) { }

    public static ProductId CreateUnique => new(Guid.NewGuid());

    public static ProductId Create(Guid value)
    {
        return new(value);
    }

    public static implicit operator ProductId(Guid id) => new(id);
    public static implicit operator Guid(ProductId id) => id.Value;
}