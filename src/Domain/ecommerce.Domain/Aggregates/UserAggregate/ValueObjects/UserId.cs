using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record UserId : AggregateRootId<Guid> {
    private UserId() { }
    private UserId(Guid value) : base(value) { }

    public static UserId CreateUnique => new(Guid.NewGuid());

    public static UserId Create(Guid value) => new(value);
    public static UserId Create(String? value) {
        if(value is null) 
            throw new ApplicationException("User not found");
        
        return new(Guid.Parse(value));
    }
}