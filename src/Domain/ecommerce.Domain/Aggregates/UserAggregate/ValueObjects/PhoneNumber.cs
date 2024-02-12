namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record PhoneNumber {
    public String? Value { get; }
    public Boolean IsConfirmed { get; private set; }

    private PhoneNumber() { }
    internal PhoneNumber(String? value) {
        this.Value = value;
        this.IsConfirmed = false;
    }
     
    internal PhoneNumber Confirm() {
        if(this.Value is null)
            throw new Exception("Invalid phone number");

        return this with {
            IsConfirmed = true
        };
    }
}