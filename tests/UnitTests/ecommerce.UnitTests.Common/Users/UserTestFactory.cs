using Bogus;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.UnitTests.Common.Users;
public static class UserTestFactory {
    public static UserAggregate CreateValidUserAggregate() {
        Faker<UserAggregate> userFaker = new Faker<UserAggregate>()
            .CustomInstantiator(faker => new UserAggregate(
                UserId.CreateUnique,
                new FullName(faker.Name.FirstName(), [], faker.Name.LastName()),
                new Email(faker.Internet.Email()),
                new PhoneNumber(faker.Phone.PhoneNumber()),
                new Password(faker.Internet.Password(), Guid.NewGuid().ToString()),
                []
            ));
        return userFaker.Generate();
    }

    public static UserAggregate CreateExpectedUserAggregate(UserId id,
                                                         FullName fullName,
                                                         Email email,
                                                         PhoneNumber phoneNumber,
                                                         Password password) {
        return new(id,
                   fullName,
                   email,
                   phoneNumber,
                   password,
                   []);
    }

    public static UserId UserId => UserId.CreateUnique;

    public static FullName CreateValidFullName() {
        Faker<FullName> fullNameFaker = new Faker<FullName>()
            .CustomInstantiator(faker => new FullName(faker.Name.FirstName(),
                                                      [faker.Name.FirstName()],
                                                      faker.Name.LastName()));
        return fullNameFaker.Generate();
    }

    public static Password CreateValidPassword() {
        Faker<Password> passwordFaker = new Faker<Password>()
            .CustomInstantiator(faker => new Password(faker.Internet.Password(),
                                                      Guid.NewGuid().ToString()));
        return passwordFaker.Generate();
    }

    public static Email CreateValidEmail() {
        Faker<Email> emailFaker = new Faker<Email>()
            .CustomInstantiator(faker => new Email(faker.Internet.Email()));
        return emailFaker.Generate();
    }

    public static PhoneNumber CreateValidPhoneNumber() {
        Faker<PhoneNumber> phoneNumberFaker = new Faker<PhoneNumber>()
            .CustomInstantiator(faker => new PhoneNumber(faker.Phone.PhoneNumber()));
        return phoneNumberFaker.Generate();
    }

    public static RefreshToken CreateValidRefreshToken() {
        Faker<RefreshToken> refreshTokenFaker = new Faker<RefreshToken>()
            .CustomInstantiator(faker => new RefreshToken(
                Guid.NewGuid().ToString(),
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(20),
                faker.Internet.Ip()));
        return refreshTokenFaker.Generate();
    }

    public static FullName CreateExpectedFullName(String firstName, String lastName) {
        return new(firstName, null, lastName);
    }

    public static Email CreateExpectedEmail(String email) {
        return new(email);
    }

    public static PhoneNumber CreateExpectedPhoneNumber(String? phoneNumber) {
        return new(phoneNumber);
    }
}