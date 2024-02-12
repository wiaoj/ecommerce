namespace ecommerce.Domain.Common.Models;
public abstract record AggregateRootId<TId> : EntityId<TId> {
    protected AggregateRootId() { }
    protected AggregateRootId(TId value) : base(value) { }
}