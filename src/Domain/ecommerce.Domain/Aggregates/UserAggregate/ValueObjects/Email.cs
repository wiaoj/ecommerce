namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record Email {
    public String Value { get; private set; }
    public Boolean IsConfirmed { get; private set; }

    private Email() { }
    internal Email(String value) {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        this.Value = value;
        this.IsConfirmed = false;
    }

    public static Email Create(String value) {
        return new(value);
    }

    internal Email Confirm() {
        return this with {
            IsConfirmed = true
        };
    }
}