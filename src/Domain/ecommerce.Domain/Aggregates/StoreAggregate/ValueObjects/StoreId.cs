using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.StoreAggregate.ValueObjects;

public sealed record StoreId : AggregateRootId<Guid>
{
    public Guid Value { get; private set; }

    public StoreId(Guid value)
    {
        Value = value;
    }
}
