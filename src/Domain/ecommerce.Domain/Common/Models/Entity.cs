namespace ecommerce.Domain.Common.Models;
public abstract class Entity<TId> : ITrackable {
    public TId Id { get; private set; }
    public DateTime LastModified { get; private set; }

    protected Entity() { }
    protected Entity(TId id) {
        this.Id = id;
    }

    public void UpdateLastModified(IDateTimeProvider dateTimeProvider) {
        this.LastModified = dateTimeProvider.UtcNow;
    }
}