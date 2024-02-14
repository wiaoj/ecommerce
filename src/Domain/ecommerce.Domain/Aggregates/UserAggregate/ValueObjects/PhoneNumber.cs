using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record PhoneNumber {
    public String? Value { get; private set; }
    public Boolean IsConfirmed { get; private set; }

    private PhoneNumber() { }
    internal PhoneNumber(String? value) {
        if(value.IsNullOrWhiteSpaces())
            value = null;
        
        this.Value = value;
        this.IsConfirmed = false;
    }

    internal PhoneNumber Confirm() {
        if(this.Value.IsNullOrEmpty())
            throw new InvalidPhoneNumberException(this.Value);

        return this with {
            IsConfirmed = true
        };
    }
}