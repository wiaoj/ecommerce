using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed partial record Email {
    public String Value { get; private set; }
    public Boolean IsConfirmed { get; private set; }

    private Email() { }
    internal Email(String value) {
        if(value is null || UserConstants.Regexes.EmailRegex().IsMatch(value).IsFalse())
            throw new InvalidEmailFormatException(value);

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