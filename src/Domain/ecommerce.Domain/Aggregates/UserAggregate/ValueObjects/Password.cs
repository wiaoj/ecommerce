using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using System;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record Password {
    public String HashedValue { get; private set; }
    public String Salt { get; private set; }

    private Password() { }
    internal Password(String hashedValue, String salt) {
        if(string.IsNullOrEmpty(hashedValue))
            throw new EmptyPasswordHashException();

        if(string.IsNullOrEmpty(salt))
            throw new EmptyPasswordSaltException();

        this.HashedValue = hashedValue;
        this.Salt = salt;
    }
}