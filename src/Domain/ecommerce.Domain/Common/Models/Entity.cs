namespace ecommerce.Domain.Common.Models;
public abstract class Entity<TId> {
    public TId Id { get; private set; }

    protected Entity() { }
    protected Entity(TId id) {
        this.Id = id;
    }
}