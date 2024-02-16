using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record UserId : AggregateRootId<Guid> {
    private UserId() { }
    internal UserId(Guid value) : base(value) { }

    public static UserId CreateUnique => new(Guid.NewGuid());

    public static UserId Create(Guid value) => new(value);
}