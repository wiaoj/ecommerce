using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed partial record FullName {
    public String FirstName { get; private set; }
    public String LastName { get; private set; }

    private FullName() { }
    internal FullName(String firstName, String lastName) {
        if(firstName is null || UserConstants.Regexes.FirstNameRegex().IsMatch(firstName).IsFalse())
            throw new InvalidFirstNameFormatException (firstName ?? String.Empty);

        if(lastName is null || UserConstants.Regexes.LastNameRegex().IsMatch(lastName).IsFalse())
            throw new InvalidLastNameFormatException(lastName ?? String.Empty);

        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public sealed override String ToString() {
        return ToWesternFormat();
    }

    public String ToWesternFormat() {
        return $"{this.FirstName} {this.LastName}";
    }

    public String ToEasternFormat() {
        return $"{this.LastName} {this.FirstName}";
    }
}