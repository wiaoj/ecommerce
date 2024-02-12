using ecommerce.Domain.Common;

namespace ecommerce.Domain.Services;
public sealed class DomainEventService : IDomainEventService {
    private readonly List<IDomainEvent> events = [];
    public IReadOnlyList<IDomainEvent> Events => this.events.AsReadOnly();

    public void AddEvent(IDomainEvent domainEvent) {
        if(this.events.Contains(domainEvent))
            return;

        this.events.Add(domainEvent);
    }

    public void AddEvents(IEnumerable<IDomainEvent> domainEvents) {
        IEnumerable<IDomainEvent> eventsToAdd = domainEvents.Except(this.events);
        this.events.AddRange(eventsToAdd);
    }

    public void ClearEvents() {
        this.events.Clear();
    }

    public void ClearEvents(IEnumerable<IDomainEvent> domainEvents) {
        foreach(IDomainEvent item in domainEvents)
            this.events.Remove(item);
    }
}