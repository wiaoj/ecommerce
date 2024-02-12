namespace ecommerce.Domain.Common.Models;
public abstract class AggregateRoot<TId, TIdType> : Entity<TId>, IHasDomainEvent where TId : AggregateRootId<TIdType> {

    private readonly HashSet<IDomainEvent> domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => this.domainEvents.ToList().AsReadOnly();

    protected AggregateRoot() { }
    protected AggregateRoot(TId id) : base(id) { }

    public abstract void Delete();

    public void RaiseDomainEvent(IDomainEvent domainEvent) {
        this.domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() {
        this.domainEvents.Clear();
    }
}