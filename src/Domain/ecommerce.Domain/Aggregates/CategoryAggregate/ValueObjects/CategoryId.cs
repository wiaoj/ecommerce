using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
public sealed record CategoryId : AggregateRootId<Guid> {
    private CategoryId() { }
    internal CategoryId(Guid value) : base(value) { }

    public static CategoryId CreateUnique => new(Guid.NewGuid());

    public static CategoryId Create(Guid value) {
        return new(value);
    }

    public static CategoryId? Create(Guid? value) {
        return value is null ? null : new(Guid.Parse(value.ToString()!));
    }

    public static implicit operator Guid?(CategoryId? id) {
        return id?.Value;
    }

    public static implicit operator Guid(CategoryId id) {
        return id.Value;
    }
}