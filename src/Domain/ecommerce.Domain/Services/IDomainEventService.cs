using ecommerce.Domain.Common;

namespace ecommerce.Domain.Services;
public interface IDomainEventService {
    IReadOnlyList<IDomainEvent> Events { get; }
    void AddEvent(IDomainEvent domainEvent);
    void AddEvents(IEnumerable<IDomainEvent> domainEvents);
    void ClearEvents();
    void ClearEvents(IEnumerable<IDomainEvent> domainEvents);
}