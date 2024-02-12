namespace ecommerce.Domain.Common;
public interface IHasDomainEvent {
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}