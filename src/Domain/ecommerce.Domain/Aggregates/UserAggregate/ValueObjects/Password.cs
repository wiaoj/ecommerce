namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record Password {
    public String HashedValue { get; private set; }
    public String Salt { get; private set; }

    private Password() { }
    internal Password(String hashedValue, String salt) {
        ArgumentException.ThrowIfNullOrEmpty(hashedValue);
        ArgumentException.ThrowIfNullOrEmpty(salt);
        this.HashedValue = hashedValue;
        this.Salt = salt;
    }
}