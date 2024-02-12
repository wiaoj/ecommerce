using MediatR;

namespace ecommerce.Domain.Common;
public interface IDomainEvent : INotification {
    Guid Id { get; }
}

public abstract record DomainEvent : IDomainEvent {
    public Guid Id { get; }

    protected DomainEvent() {
        this.Id = Guid.NewGuid();
    }

    public override Int32 GetHashCode() {
        return this.Id.GetHashCode();
    }
}