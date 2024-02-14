namespace ecommerce.Domain.Common;
public interface ITrackable {
    DateTime LastModified { get; }
    void UpdateLastModified(IDateTimeProvider dateTimeProvider);
}